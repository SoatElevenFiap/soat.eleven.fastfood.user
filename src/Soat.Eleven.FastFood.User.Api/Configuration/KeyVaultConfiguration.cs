using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Soat.Eleven.FastFood.User.Api.Configuration;

public static class KeyVaultConfiguration
{
    public static WebApplicationBuilder ConfigureKeyVault(this WebApplicationBuilder builder)
    {
        var secretKeyVaultName = builder.Configuration["KeyVault:SecretKeyName"];
        var saltKeyVaultName = builder.Configuration["KeyVault:SaltKeyName"];

        var isDevelopmentEnvironment = builder.Environment.IsDevelopment();
        TokenCredential credential;

        if (isDevelopmentEnvironment)
        {
            Console.WriteLine("🔧 Ambiente de desenvolvimento detectado - usando configuração local");

            credential = new ChainedTokenCredential(
                new AzureCliCredential(),
                new EnvironmentCredential(),
                new ManagedIdentityCredential()
            );
        }
        else
        {
            Console.WriteLine("🔧 Ambiente de desenvolvimento detectado - usando configuração de Produção");

            credential = new DefaultAzureCredential();
        }

        if (!string.IsNullOrEmpty(secretKeyVaultName))
        {
            SetKeyVault(builder, secretKeyVaultName, credential);
            Console.WriteLine("🔐 Configuração do Key Vault para SecretKey carregada com sucesso.");
        }

        if (!string.IsNullOrEmpty(saltKeyVaultName))
        {
            SetKeyVault(builder, saltKeyVaultName, credential);
            Console.WriteLine("🔐 Configuração do Key Vault para SaltKey carregada com sucesso.");
        }

        return builder;
    }

    private static void SetKeyVault(WebApplicationBuilder builder, string keyVaultName, TokenCredential credential)
    {
        builder.Configuration.AddAzureKeyVault(
            new Uri($"https://{keyVaultName}.vault.azure.net/"),
            credential,
            new KeyVaultSecretManager());
    }
}

public class KeyVaultSecretManager : Azure.Extensions.AspNetCore.Configuration.Secrets.KeyVaultSecretManager
{
    public override string GetKey(KeyVaultSecret secret)
    {
        // Converte nomes do Key Vault para formato de configuração
        // Exemplo: ConnectionString--Database vira ConnectionString:Database
        return secret.Name.Replace("--", ":");
    }
}

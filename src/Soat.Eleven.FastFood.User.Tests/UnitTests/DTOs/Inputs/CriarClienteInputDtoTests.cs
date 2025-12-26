using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Domain.Entities;
using Soat.Eleven.FastFood.User.Domain.Enums;

namespace Soat.Eleven.FastFood.User.Tests.UnitTests.DTOs.Inputs;

[TestFixture]
public class CriarClienteInputDtoTests
{
    [Test]
    public void Constructor_WhenCalled_ShouldCreateInstance()
    {
        // Act
        var dto = new CriarClienteInputDto();

        // Assert
        Assert.That(dto, Is.Not.Null);
    }

    [Test]
    public void Properties_WhenSet_ShouldReturnCorrectValues()
    {
        // Arrange
        var nome = "João Silva";
        var email = "joao@email.com";
        var senha = "123456";
        var telefone = "11999999999";
        var cpf = "12345678901";
        var dataDeNascimento = DateTime.Now.AddYears(-30);

        // Act
        var dto = new CriarClienteInputDto
        {
            Nome = nome,
            Email = email,
            Senha = senha,
            Telefone = telefone,
            Cpf = cpf,
            DataDeNascimento = dataDeNascimento
        };

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(dto.Nome, Is.EqualTo(nome));
            Assert.That(dto.Email, Is.EqualTo(email));
            Assert.That(dto.Senha, Is.EqualTo(senha));
            Assert.That(dto.Telefone, Is.EqualTo(telefone));
            Assert.That(dto.Cpf, Is.EqualTo(cpf));
            Assert.That(dto.DataDeNascimento, Is.EqualTo(dataDeNascimento));
        }
    }

    [Test]
    public void ImplicitOperator_WhenConverted_ShouldReturnCorrectCliente()
    {
        // Arrange
        var dto = new CriarClienteInputDto
        {
            Nome = "João Silva",
            Email = "joao@email.com",
            Senha = "123456",
            Telefone = "11999999999",
            Cpf = "12345678901",
            DataDeNascimento = DateTime.Now.AddYears(-30)
        };

        // Act
        Cliente cliente = dto;

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(cliente, Is.Not.Null);
            Assert.That(cliente.Usuario, Is.Not.Null);
            Assert.That(cliente.Usuario.Nome, Is.EqualTo(dto.Nome));
            Assert.That(cliente.Usuario.Email, Is.EqualTo(dto.Email));
            Assert.That(cliente.Usuario.Senha, Is.EqualTo(dto.Senha));
            Assert.That(cliente.Usuario.Telefone, Is.EqualTo(dto.Telefone));
            Assert.That(cliente.Usuario.Perfil, Is.EqualTo(PerfilUsuario.Cliente));
            Assert.That(cliente.Cpf, Is.EqualTo(dto.Cpf));
            Assert.That(cliente.DataDeNascimento, Is.EqualTo(dto.DataDeNascimento));
        }
    }

    [Test]
    public void ImplicitOperator_WhenConvertedWithNullValues_ShouldReturnClienteWithNullProperties()
    {
        // Arrange
        var dto = new CriarClienteInputDto
        {
            DataDeNascimento = default
        };

        // Act
        Cliente cliente = dto;

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(cliente, Is.Not.Null);
            Assert.That(cliente.Usuario, Is.Not.Null);
            Assert.That(cliente.Usuario.Nome, Is.Null);
            Assert.That(cliente.Usuario.Email, Is.Null);
            Assert.That(cliente.Usuario.Senha, Is.Null);
            Assert.That(cliente.Usuario.Telefone, Is.Null);
            Assert.That(cliente.Usuario.Perfil, Is.EqualTo(PerfilUsuario.Cliente));
            Assert.That(cliente.Cpf, Is.Null);
            Assert.That(cliente.DataDeNascimento, Is.Default);
        }
    }

    [Test]
    public void ImplicitOperator_WhenConvertedWithEmptyStrings_ShouldReturnClienteWithEmptyProperties()
    {
        // Arrange
        var dto = new CriarClienteInputDto
        {
            Nome = "",
            Email = "",
            Senha = "",
            Telefone = "",
            Cpf = "",
            DataDeNascimento = DateTime.MinValue
        };

        // Act
        Cliente cliente = dto;

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(cliente, Is.Not.Null);
            Assert.That(cliente.Usuario, Is.Not.Null);
            Assert.That(cliente.Usuario.Nome, Is.EqualTo(""));
            Assert.That(cliente.Usuario.Email, Is.EqualTo(""));
            Assert.That(cliente.Usuario.Senha, Is.EqualTo(""));
            Assert.That(cliente.Usuario.Telefone, Is.EqualTo(""));
            Assert.That(cliente.Usuario.Perfil, Is.EqualTo(PerfilUsuario.Cliente));
            Assert.That(cliente.Cpf, Is.EqualTo(""));
            Assert.That(cliente.DataDeNascimento, Is.EqualTo(DateTime.MinValue));
        }
    }
}
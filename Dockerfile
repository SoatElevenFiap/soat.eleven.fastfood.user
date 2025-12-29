FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /app

COPY src/*.sln src/
COPY src/Soat.Eleven.FastFood.User.Api/*.csproj src/Soat.Eleven.FastFood.User.Api/
COPY src/Soat.Eleven.FastFood.User.Application/*.csproj src/Soat.Eleven.FastFood.User.Application/
COPY src/Soat.Eleven.FastFood.User.Domain/*.csproj src/Soat.Eleven.FastFood.User.Domain/
COPY src/Soat.Eleven.FastFood.User.Infra/*.csproj src/Soat.Eleven.FastFood.User.Infra/
COPY src/Soat.Eleven.FastFood.User.Tests/*.csproj src/Soat.Eleven.FastFood.User.Tests/

RUN dotnet restore src/Soat.Eleven.FastFood.User.sln

COPY . .

RUN dotnet publish "src/Soat.Eleven.FastFood.User.Api/Soat.Eleven.FastFood.User.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ===== STAGE MIGRATOR CORRIGIDO =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS migrator

WORKDIR /app

# Copiar tudo (incluindo arquivos de projeto e migrations)
COPY --from=build-env /app/src ./src

# Instalar dotnet-ef
RUN dotnet tool install --global dotnet-ef --version 8.*

# Adicionar ao PATH
ENV PATH="/root/.dotnet/tools:${PATH}"

# Fazer restore e build dos projetos necessários para o EF funcionar
WORKDIR /app/src
RUN dotnet restore Soat.Eleven.FastFood.User.sln
RUN dotnet build Soat.Eleven.FastFood.User.Infra/Soat.Eleven.FastFood.User.Infra.csproj -c Release
RUN dotnet build Soat.Eleven.FastFood.User.Api/Soat.Eleven.FastFood.User.Api.csproj -c Release

# Voltar para /app para o comando funcionar
WORKDIR /app

# ===== STAGE FINAL =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=build-env /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "Soat.Eleven.FastFood.User.Api.dll"]

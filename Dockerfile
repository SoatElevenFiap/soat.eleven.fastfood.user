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
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS migrator

WORKDIR /app

COPY . . 

RUN dotnet tool install --global dotnet-ef --version 8.*

ENV PATH="/root/.dotnet/tools:${PATH}"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final

WORKDIR /app

COPY --from=build-env /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "Soat.Eleven.FastFood.User.Api.dll"]

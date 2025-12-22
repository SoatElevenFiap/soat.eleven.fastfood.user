# 🍔 FastFood User Microservice

> Microserviço de Gestão de Usuários do Sistema FastFood
> 
> **Projeto Avaliativo** - Pós-Graduação em Arquitetura de Software

Sistema de gerenciamento de usuários (clientes e administradores) para uma rede de fast food, desenvolvido como parte de uma arquitetura de microserviços que inclui gestão de pedidos, produtos e pagamentos.

---

## 📋 Índice

- [Sobre o Projeto](#-sobre-o-projeto)
- [Arquitetura](#-arquitetura)
- [Tecnologias](#-tecnologias)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Entidades do Domínio](#-entidades-do-domínio)
- [API Endpoints](#-api-endpoints)
- [Como Executar](#-como-executar)
- [Testes](#-testes)
- [Deploy](#-deploy)
- [Variáveis de Ambiente](#-variáveis-de-ambiente)

---

## 🎯 Sobre o Projeto

Este microserviço é responsável pela **gestão de usuários** no ecossistema FastFood, incluindo:

- ✅ Cadastro e autenticação de clientes
- ✅ Gestão de administradores do sistema
- ✅ Autenticação via JWT
- ✅ Controle de perfis e permissões
- ✅ Validação de CPF e dados cadastrais
- ✅ Integração com Azure Key Vault para segredos

### Contexto do Sistema

O sistema FastFood é composto por múltiplos microserviços:
- **User Service** (este repositório) - Gestão de usuários
- **Order Service** - Gerenciamento de pedidos
- **Product Service** - Catálogo de produtos
- **Payment Service** - Processamento de pagamentos

---

## 🏗️ Arquitetura

O projeto segue os princípios de **Clean Architecture** e **Domain-Driven Design (DDD)**:

```
┌─────────────────────────────────────────┐
│         API Layer (Controllers)         │
│  • HTTP Endpoints                       │
│  • Swagger Configuration                │
│  • JWT Authentication                   │
└─────────────┬───────────────────────────┘
              │
┌─────────────▼───────────────────────────┐
│      Application Layer (Handlers)       │
│  • Business Logic                       │
│  • DTOs & Validation                    │
│  • FluentValidation Rules               │
└─────────────┬───────────────────────────┘
              │
┌─────────────▼───────────────────────────┐
│        Domain Layer (Entities)          │
│  • Domain Models                        │
│  • Business Rules                       │
│  • Interfaces                           │
└─────────────┬───────────────────────────┘
              │
┌─────────────▼───────────────────────────┐
│     Infrastructure Layer (Data)         │
│  • Entity Framework Core                │
│  • PostgreSQL Database                  │
│  • Repositories                         │
└─────────────────────────────────────────┘
```

**Padrões Implementados:**
- Repository Pattern
- Handler Pattern (business logic)
- Dependency Injection
- Unit of Work (via EF Core)

---

## 🛠️ Tecnologias

### Core
- **.NET 8.0** - Framework principal
- **C# 12** - Linguagem de programação
- **ASP.NET Core Web API** - Framework web

### Banco de Dados
- **PostgreSQL 14** - Banco de dados relacional
- **Entity Framework Core 9.0** - ORM
- **Npgsql** - Driver PostgreSQL

### Segurança
- **JWT Bearer Authentication** - Autenticação stateless
- **Azure Key Vault** - Gerenciamento de segredos
- **HMACMD5 + Salt** - Hash de senhas

### Validação & Documentação
- **FluentValidation 12.1** - Validação de entrada
- **Swagger/OpenAPI** - Documentação interativa

### Testes
- **NUnit 4.4** - Framework de testes
- **Moq 4.20** - Biblioteca de mocking
- **AutoFixture 4.18** - Geração de dados de teste
- **Coverlet 6.0** - Cobertura de código (≥80%)

### Infraestrutura
- **Docker** - Containerização
- **Kubernetes** - Orquestração
- **Helm** - Gerenciamento de pacotes K8s
- **Azure Kubernetes Service (AKS)** - Cloud hosting

---

## 📁 Estrutura do Projeto

```
soat.eleven.fastfood.user/
│
├── src/
│   ├── Soat.Eleven.FastFood.User.sln          # Solution principal
│   │
│   ├── Soat.Eleven.FastFood.User.Api/          # 🌐 Camada de API
│   │   ├── Controllers/                        # Endpoints REST
│   │   │   ├── ClienteController.cs
│   │   │   ├── AdministradorController.cs
│   │   │   └── UsuarioController.cs
│   │   ├── Configuration/                      # Configurações
│   │   │   ├── SwaggerConfiguration.cs
│   │   │   ├── KeyVaultConfiguration.cs
│   │   │   └── RegisterServices...
│   │   ├── appsettings.json                    # Configurações
│   │   └── Program.cs                          # Entry point
│   │
│   ├── Soat.Eleven.FastFood.User.Application/ # 💼 Camada de Aplicação
│   │   ├── DTOs/                               # Data Transfer Objects
│   │   ├── Handlers/                           # Lógica de negócio
│   │   │   ├── ClienteHandler.cs
│   │   │   ├── AdministradorHandler.cs
│   │   │   └── UsuarioHandler.cs
│   │   ├── Validators/                         # Validações
│   │   └── Interfaces/                         # Contratos
│   │
│   ├── Soat.Eleven.FastFood.User.Domain/      # 🎯 Camada de Domínio
│   │   ├── Entities/                           # Entidades do negócio
│   │   │   ├── Usuario.cs
│   │   │   ├── Cliente.cs
│   │   │   └── TokenAtendimento.cs
│   │   ├── Enums/                              # Enumeradores
│   │   │   ├── Perfil.cs (Cliente, Administrador)
│   │   │   └── Status.cs (Ativo, Inativo)
│   │   └── Interfaces/                         # Interfaces do domínio
│   │
│   ├── Soat.Eleven.FastFood.User.Infra/       # 🗄️ Camada de Infraestrutura
│   │   ├── Context/                            # DbContext
│   │   │   └── ApplicationDbContext.cs
│   │   ├── Migrations/                         # Migrações EF Core
│   │   ├── Repositories/                       # Implementação de repositórios
│   │   │   ├── UsuarioRepository.cs
│   │   │   ├── ClienteRepository.cs
│   │   │   └── BaseRepository.cs
│   │   └── Services/                           # Serviços de infraestrutura
│   │       ├── JwtTokenService.cs
│   │       └── PasswordService.cs
│   │
│   └── Soat.Eleven.FastFood.User.Tests/       # 🧪 Testes
│       ├── UnitTests/
│       │   ├── Handler/                        # Testes de handlers
│       │   ├── Validators/                     # Testes de validação
│       │   ├── DTOs/                           # Testes de DTOs
│       │   └── Entities/                       # Testes de entidades
│       └── IntegrationTests/                   # Testes de integração
│
├── manifesto/                                  # 📦 Kubernetes Manifests
│   ├── fastfood-namespace.yaml
│   ├── secret.yaml
│   ├── config-map.yaml
│   ├── db-*.yaml                               # Database configs
│   ├── fastfood-*.yaml                         # Application configs
│   ├── fastfood-hpa.yaml                       # Auto-scaling
│   └── kind-config.yaml                        # Local KIND cluster
│
├── helm/                                       # ⎈ Helm Charts
│   └── fastfood-chart/
│       ├── Chart.yaml
│       ├── values.yaml
│       └── templates/                          # K8s templates
│
├── docker-compose.yml                          # 🐳 Compose local
├── Dockerfile                                  # 🐳 Multi-stage build
└── readme.md
```

---

## 🗃️ Entidades do Domínio

### Usuario
Entidade base que representa qualquer usuário do sistema.

### Cliente
Estende informações do usuário com dados específicos de clientes.

### TokenAtendimento
Representa tokens de atendimento no totem.

---

## 🚀 Como Executar

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [PostgreSQL](https://www.postgresql.org/download/) (opcional, pode usar Docker)

---

### Opção 1: Docker Compose (Recomendado)

```bash
# 1. Clone o repositório
git clone <repository-url>
cd soat.eleven.fastfood.user

# 2. Inicie o ambiente
docker-compose up -d

# 3. Acesse o Swagger
# http://localhost:5000/swagger
```

**Serviços disponíveis:**
- API: `http://localhost:5000`
- PostgreSQL: `localhost:5432`

---

### Opção 2: Execução Local (.NET CLI)

```bash
# 1. Inicie o banco de dados
docker-compose up -d db

# 2. Configure a connection string
cd src/Soat.Eleven.FastFood.User.Api
# Edite appsettings.Development.json

# 3. Execute as migrações
dotnet ef database update --project ../Soat.Eleven.FastFood.User.Infra

# 4. Inicie a aplicação
dotnet run

# 5. Acesse
# http://localhost:5000/swagger
```

---

## 🧪 Testes

### Executar Todos os Testes

```bash
cd src/Soat.Eleven.FastFood.User.Tests
dotnet test
```

### Executar com Cobertura de Código

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
```

**Requisitos de Cobertura:**
- ✅ **Mínimo:** 80% (line, branch, method)
- ✅ **Inclui:** Application e Domain layers
- ❌ **Exclui:** Tests, Program, Configuration, Migrations

### Estrutura de Testes

```
Tests/
├── UnitTests/
│   ├── Handler/
│   │   ├── ClienteHandlerTests.cs
│   │   ├── AdministradorHandlerTests.cs
│   │   └── UsuarioHandlerTests.cs
│   ├── Validators/
│   │   └── FluentValidation tests
│   ├── DTOs/
│   │   └── DTO mapping tests
│   └── Entities/
│       └── Domain entity tests
└── IntegrationTests/
    └── (pronto para testes de integração)
```

**Ferramentas:**
- **NUnit** - Framework de testes
- **Moq** - Mocking de dependências
- **AutoFixture** - Geração de dados

---

## 📄 Licença

Este projeto é parte de um trabalho acadêmico da pós-graduação em Arquitetura de Software.

---

## 📧 Contato

**Projeto Acadêmico** - Pós-Graduação em Arquitetura de Software

---

## 🎓 Contexto Acadêmico

### Objetivos do Projeto
- Aplicar conceitos de Clean Architecture e DDD
- Implementar microserviços com separação de responsabilidades
- Utilizar containers e orquestração (Docker, Kubernetes)
- Aplicar práticas de CI/CD
- Implementar autenticação e autorização
- Garantir qualidade com testes automatizados (≥80% coverage)
- Deploy em cloud (Azure Kubernetes Service)

### Tecnologias Exploradas
- ✅ .NET 8 e C# 12
- ✅ Entity Framework Core
- ✅ PostgreSQL
- ✅ Docker & Docker Compose
- ✅ Kubernetes & Helm
- ✅ Azure Cloud (AKS, ACR, Key Vault)
- ✅ JWT Authentication
- ✅ FluentValidation
- ✅ Swagger/OpenAPI
- ✅ Unit Testing com NUnit, Moq e AutoFixture

---

<div align="center">

**Desenvolvido com ❤️ para a Pós-Graduação em Arquitetura de Software**

</div>

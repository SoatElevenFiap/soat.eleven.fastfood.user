using Soat.Eleven.FastFood.User.Application.DTOs.Outputs;
using Soat.Eleven.FastFood.User.Domain.Entities;
using Soat.Eleven.FastFood.User.Domain.Enums;

namespace Soat.Eleven.FastFood.User.Tests.UnitTests.DTOs.Outputs;

[TestFixture]
public class UsuarioOutputDtoTests
{
    [Test]
    public void Constructor_WhenCalled_ShouldCreateInstance()
    {
        // Act
        var dto = new UsuarioOutputDto();

        // Assert
        Assert.That(dto, Is.Not.Null);
    }

    [Test]
    public void Properties_WhenSet_ShouldReturnCorrectValues()
    {
        // Arrange
        var id = Guid.NewGuid();
        var clientId = Guid.NewGuid();
        var nome = "João Silva";
        var email = "joao@email.com";
        var telefone = "11999999999";
        var cpf = "12345678901";
        var dataDeNascimento = DateTime.Now.AddYears(-30);

        // Act
        var dto = new UsuarioOutputDto
        {
            Id = id,
            ClientId = clientId,
            Nome = nome,
            Email = email,
            Telefone = telefone,
            Cpf = cpf,
            DataDeNascimento = dataDeNascimento
        };

        // Assert
        Assert.That(dto.Id, Is.EqualTo(id));
        Assert.That(dto.ClientId, Is.EqualTo(clientId));
        Assert.That(dto.Nome, Is.EqualTo(nome));
        Assert.That(dto.Email, Is.EqualTo(email));
        Assert.That(dto.Telefone, Is.EqualTo(telefone));
        Assert.That(dto.Cpf, Is.EqualTo(cpf));
        Assert.That(dto.DataDeNascimento, Is.EqualTo(dataDeNascimento));
    }

    [Test]
    public void ExplicitOperator_WhenConvertedFromUsuarioWithCliente_ShouldReturnCorrectDto()
    {
        // Arrange
        var usuarioId = Guid.NewGuid();
        var usuario = new Usuario
        {
            Id = usuarioId,
            Nome = "João Silva",
            Email = "joao@email.com",
            Telefone = "11999999999",
            Perfil = PerfilUsuario.Cliente,
            Cliente = new Cliente
            {
                Id = Guid.NewGuid(),
                Cpf = "12345678901",
                DataDeNascimento = DateTime.Now.AddYears(-30)
            }
        };

        // Act
        var dto = (UsuarioOutputDto)usuario;

        // Assert
        Assert.That(dto, Is.Not.Null);
        Assert.That(dto.Id, Is.EqualTo(usuarioId));
        Assert.That(dto.Nome, Is.EqualTo(usuario.Nome));
        Assert.That(dto.Email, Is.EqualTo(usuario.Email));
        Assert.That(dto.Telefone, Is.EqualTo(usuario.Telefone));
        Assert.That(dto.Cpf, Is.EqualTo(usuario.Cliente.Cpf));
        Assert.That(dto.DataDeNascimento, Is.EqualTo(usuario.Cliente.DataDeNascimento));
    }

    [Test]
    public void ExplicitOperator_WhenConvertedFromUsuarioWithoutCliente_ShouldReturnDtoWithEmptyClienteData()
    {
        // Arrange
        var usuarioId = Guid.NewGuid();
        var usuario = new Usuario
        {
            Id = usuarioId,
            Nome = "Admin Silva",
            Email = "admin@email.com",
            Telefone = "11888888888",
            Perfil = PerfilUsuario.Administrador,
            Cliente = null
        };

        // Act
        var dto = (UsuarioOutputDto)usuario;

        // Assert
        Assert.That(dto, Is.Not.Null);
        Assert.That(dto.Id, Is.EqualTo(usuarioId));
        Assert.That(dto.Nome, Is.EqualTo(usuario.Nome));
        Assert.That(dto.Email, Is.EqualTo(usuario.Email));
        Assert.That(dto.Telefone, Is.EqualTo(usuario.Telefone));
        Assert.That(dto.Cpf, Is.EqualTo(string.Empty));
        Assert.That(dto.DataDeNascimento, Is.EqualTo(DateTime.MinValue));
    }

    [Test]
    public void ExplicitOperator_WhenConvertedFromUsuarioWithClienteHavingNullCpf_ShouldReturnDtoWithEmptyCpf()
    {
        // Arrange
        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = "João Silva",
            Email = "joao@email.com",
            Telefone = "11999999999",
            Perfil = PerfilUsuario.Cliente,
            Cliente = new Cliente
            {
                Id = Guid.NewGuid(),
                Cpf = null,
                DataDeNascimento = DateTime.Now.AddYears(-30)
            }
        };

        // Act
        var dto = (UsuarioOutputDto)usuario;

        // Assert
        Assert.That(dto, Is.Not.Null);
        Assert.That(dto.Cpf, Is.EqualTo(string.Empty));
    }

    [Test]
    public void ExplicitOperator_WhenConvertedFromUsuarioWithEmptyValues_ShouldReturnDtoWithEmptyValues()
    {
        // Arrange
        var usuario = new Usuario
        {
            Id = Guid.Empty,
            Nome = "",
            Email = "",
            Telefone = "",
            Perfil = PerfilUsuario.Cliente,
            Cliente = new Cliente
            {
                Id = Guid.Empty,
                Cpf = "",
                DataDeNascimento = DateTime.MinValue
            }
        };

        // Act
        var dto = (UsuarioOutputDto)usuario;

        // Assert
        Assert.That(dto, Is.Not.Null);
        Assert.That(dto.Id, Is.EqualTo(Guid.Empty));
        Assert.That(dto.Nome, Is.EqualTo(""));
        Assert.That(dto.Email, Is.EqualTo(""));
        Assert.That(dto.Telefone, Is.EqualTo(""));
        Assert.That(dto.Cpf, Is.EqualTo(""));
        Assert.That(dto.DataDeNascimento, Is.EqualTo(DateTime.MinValue));
    }

    [Test]
    public void ExplicitOperator_WhenConvertedFromUsuarioWithNullProperties_ShouldReturnDtoWithNullProperties()
    {
        // Arrange
        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = null,
            Email = null,
            Telefone = null,
            Perfil = PerfilUsuario.Administrador,
            Cliente = null
        };

        // Act
        var dto = (UsuarioOutputDto)usuario;

        // Assert
        Assert.That(dto, Is.Not.Null);
        Assert.That(dto.Nome, Is.Null);
        Assert.That(dto.Email, Is.Null);
        Assert.That(dto.Telefone, Is.Null);
        Assert.That(dto.Cpf, Is.EqualTo(string.Empty));
        Assert.That(dto.DataDeNascimento, Is.EqualTo(DateTime.MinValue));
    }

    [Test]
    public void ExplicitOperator_WhenConvertedMultipleTimes_ShouldReturnDifferentInstances()
    {
        // Arrange
        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = "João Silva",
            Email = "joao@email.com",
            Telefone = "11999999999",
            Perfil = PerfilUsuario.Cliente,
            Cliente = new Cliente
            {
                Id = Guid.NewGuid(),
                Cpf = "12345678901",
                DataDeNascimento = DateTime.Now.AddYears(-30)
            }
        };

        // Act
        var dto1 = (UsuarioOutputDto)usuario;
        var dto2 = (UsuarioOutputDto)usuario;

        // Assert
        Assert.That(dto1, Is.Not.Null);
        Assert.That(dto2, Is.Not.Null);
        Assert.That(dto1, Is.Not.SameAs(dto2));
        Assert.That(dto1.Id, Is.EqualTo(dto2.Id));
        Assert.That(dto1.Nome, Is.EqualTo(dto2.Nome));
        Assert.That(dto1.Email, Is.EqualTo(dto2.Email));
        Assert.That(dto1.Telefone, Is.EqualTo(dto2.Telefone));
        Assert.That(dto1.Cpf, Is.EqualTo(dto2.Cpf));
        Assert.That(dto1.DataDeNascimento, Is.EqualTo(dto2.DataDeNascimento));
    }
}
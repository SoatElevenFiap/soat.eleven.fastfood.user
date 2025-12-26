using Soat.Eleven.FastFood.User.Application.DTOs.Outputs;
using Soat.Eleven.FastFood.User.Domain.Entities;
using Soat.Eleven.FastFood.User.Domain.Enums;

namespace Soat.Eleven.FastFood.User.Tests.UnitTests.DTOs.Outputs;

[TestFixture]
public class UsuarioAdmOutputDtoTests
{
    [Test]
    public void Constructor_WhenCalled_ShouldCreateInstance()
    {
        // Act
        var dto = new UsuarioAdmOutputDto();

        // Assert
        Assert.That(dto, Is.Not.Null);
    }

    [Test]
    public void Properties_WhenSet_ShouldReturnCorrectValues()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Admin Silva";
        var email = "admin@email.com";
        var telefone = "11999999999";

        // Act
        var dto = new UsuarioAdmOutputDto
        {
            Id = id,
            Nome = nome,
            Email = email,
            Telefone = telefone
        };

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(dto.Id, Is.EqualTo(id));
            Assert.That(dto.Nome, Is.EqualTo(nome));
            Assert.That(dto.Email, Is.EqualTo(email));
            Assert.That(dto.Telefone, Is.EqualTo(telefone));
        }
    }

    [Test]
    public void ExplicitOperator_WhenConvertedFromUsuario_ShouldReturnCorrectDto()
    {
        // Arrange
        var usuarioId = Guid.NewGuid();
        var usuario = new Usuario
        {
            Id = usuarioId,
            Nome = "Admin Silva",
            Email = "admin@email.com",
            Telefone = "11999999999",
            Perfil = PerfilUsuario.Administrador
        };

        // Act
        var dto = (UsuarioAdmOutputDto)usuario;

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(dto, Is.Not.Null);
            Assert.That(dto.Id, Is.EqualTo(usuarioId));
            Assert.That(dto.Nome, Is.EqualTo(usuario.Nome));
            Assert.That(dto.Email, Is.EqualTo(usuario.Email));
            Assert.That(dto.Telefone, Is.EqualTo(usuario.Telefone));
        }
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
            Perfil = PerfilUsuario.Administrador
        };

        // Act
        var dto = (UsuarioAdmOutputDto)usuario;

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(dto, Is.Not.Null);
            Assert.That(dto.Id, Is.EqualTo(Guid.Empty));
            Assert.That(dto.Nome, Is.EqualTo(""));
            Assert.That(dto.Email, Is.EqualTo(""));
            Assert.That(dto.Telefone, Is.EqualTo(""));
        }
    }

    [Test]
    public void ExplicitOperator_WhenConvertedFromUsuarioWithNullProperties_ShouldReturnDtoWithNullProperties()
    {
        // Arrange
        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Perfil = PerfilUsuario.Administrador
        };

        // Act
        var dto = (UsuarioAdmOutputDto)usuario;

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(dto, Is.Not.Null);
            Assert.That(dto.Nome, Is.Null);
            Assert.That(dto.Email, Is.Null);
            Assert.That(dto.Telefone, Is.Null);
        }
    }

    [Test]
    public void ExplicitOperator_WhenConvertedFromUsuarioWithClientePerfil_ShouldStillConvert()
    {
        // Arrange
        var usuarioId = Guid.NewGuid();
        var usuario = new Usuario
        {
            Id = usuarioId,
            Nome = "João Silva",
            Email = "joao@email.com",
            Telefone = "11888888888",
            Perfil = PerfilUsuario.Cliente // Different profile, but conversion should still work
        };

        // Act
        var dto = (UsuarioAdmOutputDto)usuario;

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(dto, Is.Not.Null);
            Assert.That(dto.Id, Is.EqualTo(usuarioId));
            Assert.That(dto.Nome, Is.EqualTo(usuario.Nome));
            Assert.That(dto.Email, Is.EqualTo(usuario.Email));
            Assert.That(dto.Telefone, Is.EqualTo(usuario.Telefone));
        }
    }

    [Test]
    public void ExplicitOperator_WhenConvertedWithWhitespaceValues_ShouldReturnDtoWithWhitespace()
    {
        // Arrange
        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = "   Admin Silva   ",
            Email = "  admin@email.com  ",
            Telefone = " 11999999999 ",
            Perfil = PerfilUsuario.Administrador
        };

        // Act
        var dto = (UsuarioAdmOutputDto)usuario;

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(dto, Is.Not.Null);
            Assert.That(dto.Nome, Is.EqualTo("   Admin Silva   "));
            Assert.That(dto.Email, Is.EqualTo("  admin@email.com  "));
            Assert.That(dto.Telefone, Is.EqualTo(" 11999999999 "));
        }
    }

    [Test]
    public void ExplicitOperator_WhenConvertedMultipleTimes_ShouldReturnDifferentInstances()
    {
        // Arrange
        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = "Admin Silva",
            Email = "admin@email.com",
            Telefone = "11999999999",
            Perfil = PerfilUsuario.Administrador
        };

        // Act
        var dto1 = (UsuarioAdmOutputDto)usuario;
        var dto2 = (UsuarioAdmOutputDto)usuario;

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(dto1, Is.Not.Null);
            Assert.That(dto2, Is.Not.Null);
            Assert.That(dto1, Is.Not.SameAs(dto2));
            Assert.That(dto1.Id, Is.EqualTo(dto2.Id));
            Assert.That(dto1.Nome, Is.EqualTo(dto2.Nome));
            Assert.That(dto1.Email, Is.EqualTo(dto2.Email));
            Assert.That(dto1.Telefone, Is.EqualTo(dto2.Telefone));
        }
    }

    [Test]
    public void Properties_WhenSetWithLongValues_ShouldReturnLongValues()
    {
        // Arrange
        var id = Guid.NewGuid();
        var longNome = new string('A', 200);
        var longEmail = new string('B', 100) + "@email.com";
        var longTelefone = new string('9', 50);

        // Act
        var dto = new UsuarioAdmOutputDto
        {
            Id = id,
            Nome = longNome,
            Email = longEmail,
            Telefone = longTelefone
        };

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(dto.Id, Is.EqualTo(id));
            Assert.That(dto.Nome, Is.EqualTo(longNome));
            Assert.That(dto.Email, Is.EqualTo(longEmail));
            Assert.That(dto.Telefone, Is.EqualTo(longTelefone));
            Assert.That(dto.Nome.Length, Is.EqualTo(200));
            Assert.That(dto.Email.Length, Is.EqualTo(110));
            Assert.That(dto.Telefone.Length, Is.EqualTo(50));
        }
    }
}
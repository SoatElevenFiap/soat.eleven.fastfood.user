using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Domain.Entities;
using Soat.Eleven.FastFood.User.Domain.Enums;

namespace Soat.Eleven.FastFood.User.Tests.UnitTests.DTOs.Inputs;

[TestFixture]
public class CriarAdmInputDtoTests
{
    [Test]
    public void Constructor_WhenCalled_ShouldCreateInstance()
    {
        // Act
        var dto = new CriarAdmInputDto();

        // Assert
        Assert.That(dto, Is.Not.Null);
    }

    [Test]
    public void Properties_WhenSet_ShouldReturnCorrectValues()
    {
        // Arrange
        var nome = "Admin Silva";
        var email = "admin@email.com";
        var senha = "123456";
        var telefone = "11999999999";

        // Act
        var dto = new CriarAdmInputDto
        {
            Nome = nome,
            Email = email,
            Senha = senha,
            Telefone = telefone
        };

        // Assert
        Assert.That(dto.Nome, Is.EqualTo(nome));
        Assert.That(dto.Email, Is.EqualTo(email));
        Assert.That(dto.Senha, Is.EqualTo(senha));
        Assert.That(dto.Telefone, Is.EqualTo(telefone));
    }

    [Test]
    public void ImplicitOperator_WhenConverted_ShouldReturnCorrectUsuario()
    {
        // Arrange
        var dto = new CriarAdmInputDto
        {
            Nome = "Admin Silva",
            Email = "admin@email.com",
            Senha = "123456",
            Telefone = "11999999999"
        };

        // Act
        Usuario usuario = dto;

        // Assert
        Assert.That(usuario, Is.Not.Null);
        Assert.That(usuario.Nome, Is.EqualTo(dto.Nome));
        Assert.That(usuario.Email, Is.EqualTo(dto.Email));
        Assert.That(usuario.Senha, Is.EqualTo(dto.Senha));
        Assert.That(usuario.Telefone, Is.EqualTo(dto.Telefone));
        Assert.That(usuario.Perfil, Is.EqualTo(PerfilUsuario.Administrador));
    }

    [Test]
    public void ImplicitOperator_WhenConvertedWithNullValues_ShouldReturnUsuarioWithNullProperties()
    {
        // Arrange
        var dto = new CriarAdmInputDto
        {
            Nome = string.Empty,
            Email = string.Empty,
            Senha = string.Empty,
            Telefone = string.Empty
        };

        // Act
        Usuario usuario = dto;

        // Assert
        Assert.That(usuario, Is.Not.Null);
        Assert.That(usuario.Nome, Is.EqualTo(string.Empty));
        Assert.That(usuario.Email, Is.EqualTo(string.Empty));
        Assert.That(usuario.Senha, Is.EqualTo(string.Empty));
        Assert.That(usuario.Telefone, Is.EqualTo(string.Empty));
        Assert.That(usuario.Perfil, Is.EqualTo(PerfilUsuario.Administrador));
    }

    [Test]
    public void ImplicitOperator_WhenConvertedWithEmptyStrings_ShouldReturnUsuarioWithEmptyProperties()
    {
        // Arrange
        var dto = new CriarAdmInputDto
        {
            Nome = "",
            Email = "",
            Senha = "",
            Telefone = ""
        };

        // Act
        Usuario usuario = dto;

        // Assert
        Assert.That(usuario, Is.Not.Null);
        Assert.That(usuario.Nome, Is.EqualTo(""));
        Assert.That(usuario.Email, Is.EqualTo(""));
        Assert.That(usuario.Senha, Is.EqualTo(""));
        Assert.That(usuario.Telefone, Is.EqualTo(""));
        Assert.That(usuario.Perfil, Is.EqualTo(PerfilUsuario.Administrador));
    }

    [Test]
    public void ImplicitOperator_WhenConvertedMultipleTimes_ShouldReturnDifferentInstances()
    {
        // Arrange
        var dto = new CriarAdmInputDto
        {
            Nome = "Admin Silva",
            Email = "admin@email.com",
            Senha = "123456",
            Telefone = "11999999999"
        };

        // Act
        Usuario usuario1 = dto;
        Usuario usuario2 = dto;

        // Assert
        Assert.That(usuario1, Is.Not.Null);
        Assert.That(usuario2, Is.Not.Null);
        Assert.That(usuario1, Is.Not.SameAs(usuario2));
        Assert.That(usuario1.Nome, Is.EqualTo(usuario2.Nome));
        Assert.That(usuario1.Email, Is.EqualTo(usuario2.Email));
        Assert.That(usuario1.Senha, Is.EqualTo(usuario2.Senha));
        Assert.That(usuario1.Telefone, Is.EqualTo(usuario2.Telefone));
        Assert.That(usuario1.Perfil, Is.EqualTo(usuario2.Perfil));
    }
}
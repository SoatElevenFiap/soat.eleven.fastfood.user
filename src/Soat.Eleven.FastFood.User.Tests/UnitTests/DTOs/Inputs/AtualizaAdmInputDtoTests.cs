using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;

namespace Soat.Eleven.FastFood.User.Tests.UnitTests.DTOs.Inputs;

[TestFixture]
public class AtualizaAdmInputDtoTests
{
    [Test]
    public void Constructor_WhenCalled_ShouldCreateInstance()
    {
        // Act
        var dto = new AtualizaAdmInputDto();

        // Assert
        Assert.That(dto, Is.Not.Null);
    }

    [Test]
    public void Properties_WhenSet_ShouldReturnCorrectValues()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Admin Silva Atualizado";
        var email = "admin.novo@email.com";
        var telefone = "11888888888";

        // Act
        var dto = new AtualizaAdmInputDto
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
    public void Properties_WhenSetToNull_ShouldReturnNull()
    {
        // Act
        var dto = new AtualizaAdmInputDto
        {
            Id = Guid.Empty
        };

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(dto.Id, Is.EqualTo(Guid.Empty));
            Assert.That(dto.Nome, Is.Null);
            Assert.That(dto.Email, Is.Null);
            Assert.That(dto.Telefone, Is.Null);
        }
    }

    [Test]
    public void Properties_WhenSetToEmptyStrings_ShouldReturnEmptyStrings()
    {
        // Act
        var dto = new AtualizaAdmInputDto
        {
            Id = Guid.NewGuid(),
            Nome = "",
            Email = "",
            Telefone = ""
        };

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(dto.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(dto.Nome, Is.EqualTo(""));
            Assert.That(dto.Email, Is.EqualTo(""));
            Assert.That(dto.Telefone, Is.EqualTo(""));
        }
    }

    [Test]
    public void Properties_WhenSetWithWhitespace_ShouldReturnWhitespace()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var dto = new AtualizaAdmInputDto
        {
            Id = id,
            Nome = "   ",
            Email = "  admin@email.com  ",
            Telefone = " 11999999999 "
        };

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(dto.Id, Is.EqualTo(id));
            Assert.That(dto.Nome, Is.EqualTo("   "));
            Assert.That(dto.Email, Is.EqualTo("  admin@email.com  "));
            Assert.That(dto.Telefone, Is.EqualTo(" 11999999999 "));
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
        var dto = new AtualizaAdmInputDto
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
        }
    }

    [Test]
    public void Equality_WhenComparingDifferentInstances_ShouldNotBeEqual()
    {
        // Arrange
        var id = Guid.NewGuid();
        var dto1 = new AtualizaAdmInputDto
        {
            Id = id,
            Nome = "Admin",
            Email = "admin@email.com",
            Telefone = "11999999999"
        };

        var dto2 = new AtualizaAdmInputDto
        {
            Id = id,
            Nome = "Admin",
            Email = "admin@email.com",
            Telefone = "11999999999"
        };

        // Act & Assert
        Assert.That(dto1, Is.Not.SameAs(dto2));
        using (Assert.EnterMultipleScope())
        {
            Assert.That(dto1.Id, Is.EqualTo(dto2.Id));
            Assert.That(dto1.Nome, Is.EqualTo(dto2.Nome));
            Assert.That(dto1.Email, Is.EqualTo(dto2.Email));
            Assert.That(dto1.Telefone, Is.EqualTo(dto2.Telefone));
        }
    }
}
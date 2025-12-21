using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;

namespace Soat.Eleven.FastFood.User.Tests.UnitTests.DTOs.Inputs;

[TestFixture]
public class AtualizarSenhaInputDtoTests
{
    [Test]
    public void Constructor_WhenCalled_ShouldCreateInstance()
    {
        // Act
        var dto = new AtualizarSenhaInputDto();

        // Assert
        Assert.That(dto, Is.Not.Null);
    }

    [Test]
    public void Properties_WhenSet_ShouldReturnCorrectValues()
    {
        // Arrange
        var currentPassword = "senhaAtual123";
        var newPassword = "novaSenha456";

        // Act
        var dto = new AtualizarSenhaInputDto
        {
            CurrentPassword = currentPassword,
            NewPassword = newPassword
        };

        // Assert
        Assert.That(dto.CurrentPassword, Is.EqualTo(currentPassword));
        Assert.That(dto.NewPassword, Is.EqualTo(newPassword));
    }

    [Test]
    public void Properties_WhenSetToNull_ShouldReturnNull()
    {
        // Act
        var dto = new AtualizarSenhaInputDto
        {
            CurrentPassword = null,
            NewPassword = null
        };

        // Assert
        Assert.That(dto.CurrentPassword, Is.Null);
        Assert.That(dto.NewPassword, Is.Null);
    }

    [Test]
    public void Properties_WhenSetToEmptyStrings_ShouldReturnEmptyStrings()
    {
        // Act
        var dto = new AtualizarSenhaInputDto
        {
            CurrentPassword = "",
            NewPassword = ""
        };

        // Assert
        Assert.That(dto.CurrentPassword, Is.EqualTo(""));
        Assert.That(dto.NewPassword, Is.EqualTo(""));
    }

    [Test]
    public void Properties_WhenSetToSameValue_ShouldReturnSameValue()
    {
        // Arrange
        var password = "mesmaSenha123";

        // Act
        var dto = new AtualizarSenhaInputDto
        {
            CurrentPassword = password,
            NewPassword = password
        };

        // Assert
        Assert.That(dto.CurrentPassword, Is.EqualTo(password));
        Assert.That(dto.NewPassword, Is.EqualTo(password));
        Assert.That(dto.CurrentPassword, Is.EqualTo(dto.NewPassword));
    }

    [Test]
    public void Properties_WhenSetWithWhitespace_ShouldReturnWhitespace()
    {
        // Act
        var dto = new AtualizarSenhaInputDto
        {
            CurrentPassword = "   senha atual   ",
            NewPassword = "  nova senha  "
        };

        // Assert
        Assert.That(dto.CurrentPassword, Is.EqualTo("   senha atual   "));
        Assert.That(dto.NewPassword, Is.EqualTo("  nova senha  "));
    }

    [Test]
    public void Properties_WhenSetWithSpecialCharacters_ShouldReturnSpecialCharacters()
    {
        // Arrange
        var currentPassword = "senha@123!#$";
        var newPassword = "nova&senha*456%";

        // Act
        var dto = new AtualizarSenhaInputDto
        {
            CurrentPassword = currentPassword,
            NewPassword = newPassword
        };

        // Assert
        Assert.That(dto.CurrentPassword, Is.EqualTo(currentPassword));
        Assert.That(dto.NewPassword, Is.EqualTo(newPassword));
    }

    [Test]
    public void Properties_WhenSetWithLongPasswords_ShouldReturnLongPasswords()
    {
        // Arrange
        var currentPassword = new string('A', 100) + "123";
        var newPassword = new string('B', 200) + "456";

        // Act
        var dto = new AtualizarSenhaInputDto
        {
            CurrentPassword = currentPassword,
            NewPassword = newPassword
        };

        // Assert
        Assert.That(dto.CurrentPassword, Is.EqualTo(currentPassword));
        Assert.That(dto.NewPassword, Is.EqualTo(newPassword));
        Assert.That(dto.CurrentPassword.Length, Is.EqualTo(103));
        Assert.That(dto.NewPassword.Length, Is.EqualTo(203));
    }

    [Test]
    public void Properties_WhenSetWithMinimumLength_ShouldReturnMinimumLength()
    {
        // Arrange
        var currentPassword = "A";
        var newPassword = "B";

        // Act
        var dto = new AtualizarSenhaInputDto
        {
            CurrentPassword = currentPassword,
            NewPassword = newPassword
        };

        // Assert
        Assert.That(dto.CurrentPassword, Is.EqualTo(currentPassword));
        Assert.That(dto.NewPassword, Is.EqualTo(newPassword));
        Assert.That(dto.CurrentPassword.Length, Is.EqualTo(1));
        Assert.That(dto.NewPassword.Length, Is.EqualTo(1));
    }

    [Test]
    public void Equality_WhenComparingDifferentInstances_ShouldNotBeEqual()
    {
        // Arrange
        var dto1 = new AtualizarSenhaInputDto
        {
            CurrentPassword = "senha123",
            NewPassword = "novaSenha456"
        };

        var dto2 = new AtualizarSenhaInputDto
        {
            CurrentPassword = "senha123",
            NewPassword = "novaSenha456"
        };

        // Act & Assert
        Assert.That(dto1, Is.Not.SameAs(dto2));
        Assert.That(dto1.CurrentPassword, Is.EqualTo(dto2.CurrentPassword));
        Assert.That(dto1.NewPassword, Is.EqualTo(dto2.NewPassword));
    }
}
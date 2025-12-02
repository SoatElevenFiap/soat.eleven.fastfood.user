using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.Validators;

namespace Soat.Eleven.FastFood.User.Tests.UnitTests.Validators;

[TestFixture]
public class AtualizarSenhaValidatorTests
{
    private AtualizarSenhaValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new AtualizarSenhaValidator();
    }

    [Test]
    public void Validate_WhenAllFieldsValid_ShouldReturnTrue()
    {
        // Arrange
        var dto = new AtualizarSenhaInputDto
        {
            CurrentPassword = "senhaAtual123",
            NewPassword = "novaSenha456"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Empty);
    }

    [Test]
    public void Validate_WhenCurrentPasswordIsEmpty_ShouldReturnError()
    {
        // Arrange
        var dto = new AtualizarSenhaInputDto
        {
            CurrentPassword = "",
            NewPassword = "novaSenha456"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "A senha atual é obrigatória."), Is.True);
    }

    [Test]
    public void Validate_WhenNewPasswordIsEmpty_ShouldReturnError()
    {
        // Arrange
        var dto = new AtualizarSenhaInputDto
        {
            CurrentPassword = "senhaAtual123",
            NewPassword = ""
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "A nova senha é obrigatória."), Is.True);
    }

    [Test]
    public void Validate_WhenNewPasswordIsTooShort_ShouldReturnError()
    {
        // Arrange
        var dto = new AtualizarSenhaInputDto
        {
            CurrentPassword = "senhaAtual123",
            NewPassword = "123" // Less than 6 characters
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "A nova senha deve ter no mínimo 6 caracteres."), Is.True);
    }

    [Test]
    public void Validate_WhenNewPasswordIsSameAsCurrentPassword_ShouldReturnError()
    {
        // Arrange
        var dto = new AtualizarSenhaInputDto
        {
            CurrentPassword = "mesmaSenha123",
            NewPassword = "mesmaSenha123" // Same as current password
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "A nova senha deve ser diferente da senha atual."), Is.True);
    }

    [Test]
    public void Validate_WhenNewPasswordIsMinimumLengthAndDifferent_ShouldReturnTrue()
    {
        // Arrange
        var dto = new AtualizarSenhaInputDto
        {
            CurrentPassword = "senhaAtual",
            NewPassword = "123456" // Exactly 6 characters and different
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Empty);
    }

    [Test]
    public void Validate_WhenBothPasswordsAreEmpty_ShouldReturnMultipleErrors()
    {
        // Arrange
        var dto = new AtualizarSenhaInputDto
        {
            CurrentPassword = "",
            NewPassword = ""
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Count.GreaterThanOrEqualTo(2));
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "A senha atual é obrigatória."), Is.True);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "A nova senha é obrigatória."), Is.True);
    }

    [Test]
    public void Validate_WhenNewPasswordIsNull_ShouldReturnError()
    {
        // Arrange
        var dto = new AtualizarSenhaInputDto
        {
            CurrentPassword = "senhaAtual123",
            NewPassword = string.Empty
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "A nova senha é obrigatória."), Is.True);
    }

    [Test]
    public void Validate_WhenCurrentPasswordIsNull_ShouldReturnError()
    {
        // Arrange
        var dto = new AtualizarSenhaInputDto
        {
            CurrentPassword = string.Empty,
            NewPassword = "novaSenha456"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "A senha atual é obrigatória."), Is.True);
    }

    [Test]
    public void Validate_WhenNewPasswordHasWhitespaceOnly_ShouldReturnError()
    {
        // Arrange
        var dto = new AtualizarSenhaInputDto
        {
            CurrentPassword = "senhaAtual123",
            NewPassword = "      " // Only whitespace
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "A nova senha é obrigatória."), Is.True);
    }
}
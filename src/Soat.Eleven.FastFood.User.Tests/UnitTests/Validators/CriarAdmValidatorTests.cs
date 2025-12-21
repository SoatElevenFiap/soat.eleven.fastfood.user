using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.Validators;

namespace Soat.Eleven.FastFood.User.Tests.UnitTests.Validators;

[TestFixture]
public class CriarAdmValidatorTests
{
    private CriarAdmValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new CriarAdmValidator();
    }

    [Test]
    public void Validate_WhenAllFieldsValid_ShouldReturnTrue()
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
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Empty);
    }

    [Test]
    public void Validate_WhenNomeIsEmpty_ShouldReturnError()
    {
        // Arrange
        var dto = new CriarAdmInputDto
        {
            Nome = "",
            Email = "admin@email.com",
            Senha = "123456",
            Telefone = "11999999999"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O nome é obrigatório."), Is.True);
    }

    [Test]
    public void Validate_WhenNomeExceedsMaxLength_ShouldReturnError()
    {
        // Arrange
        var dto = new CriarAdmInputDto
        {
            Nome = new string('A', 101), // 101 characters
            Email = "admin@email.com",
            Senha = "123456",
            Telefone = "11999999999"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O nome deve ter no máximo 100 caracteres."), Is.True);
    }

    [Test]
    public void Validate_WhenEmailIsEmpty_ShouldReturnError()
    {
        // Arrange
        var dto = new CriarAdmInputDto
        {
            Nome = "Admin Silva",
            Email = "",
            Senha = "123456",
            Telefone = "11999999999"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O email é obrigatório."), Is.True);
    }

    [Test]
    public void Validate_WhenEmailIsInvalid_ShouldReturnError()
    {
        // Arrange
        var dto = new CriarAdmInputDto
        {
            Nome = "Admin Silva",
            Email = "email_invalido",
            Senha = "123456",
            Telefone = "11999999999"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O email deve ser um endereço válido."), Is.True);
    }

    [Test]
    public void Validate_WhenSenhaIsEmpty_ShouldReturnError()
    {
        // Arrange
        var dto = new CriarAdmInputDto
        {
            Nome = "Admin Silva",
            Email = "admin@email.com",
            Senha = "",
            Telefone = "11999999999"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "A senha é obrigatória."), Is.True);
    }

    [Test]
    public void Validate_WhenSenhaIsTooShort_ShouldReturnError()
    {
        // Arrange
        var dto = new CriarAdmInputDto
        {
            Nome = "Admin Silva",
            Email = "admin@email.com",
            Senha = "123", // Less than 6 characters
            Telefone = "11999999999"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "A senha deve ter no mínimo 6 caracteres."), Is.True);
    }

    [Test]
    public void Validate_WhenTelefoneIsEmpty_ShouldReturnError()
    {
        // Arrange
        var dto = new CriarAdmInputDto
        {
            Nome = "Admin Silva",
            Email = "admin@email.com",
            Senha = "123456",
            Telefone = ""
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O telefone é obrigatório."), Is.True);
    }

    [Test]
    public void Validate_WhenTelefoneExceedsMaxLength_ShouldReturnError()
    {
        // Arrange
        var dto = new CriarAdmInputDto
        {
            Nome = "Admin Silva",
            Email = "admin@email.com",
            Senha = "123456",
            Telefone = "1234567890123456" // 16 characters
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O telefone deve ter no máximo 15 caracteres."), Is.True);
    }

    [Test]
    public void Validate_WhenMultipleFieldsAreInvalid_ShouldReturnMultipleErrors()
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
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Count.GreaterThanOrEqualTo(4));
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O nome é obrigatório."), Is.True);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O email é obrigatório."), Is.True);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "A senha é obrigatória."), Is.True);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O telefone é obrigatório."), Is.True);
    }

    [Test]
    public void Validate_WhenNomeIsValidAndEmailIsValidAndSenhaIsValidAndTelefoneIsValid_ShouldReturnTrue()
    {
        // Arrange
        var dto = new CriarAdmInputDto
        {
            Nome = "A", // Minimum valid length
            Email = "a@b.co", // Minimum valid email
            Senha = "123456", // Minimum valid length
            Telefone = "1" // Minimum valid length
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Empty);
    }
}
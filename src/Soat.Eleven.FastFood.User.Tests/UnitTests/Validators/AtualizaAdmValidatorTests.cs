using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.Validators;
using System.Linq;

namespace Soat.Eleven.FastFood.User.Tests.UnitTests.Validators;

[TestFixture]
public class AtualizaAdmValidatorTests
{
    private AtualizaAdmValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new AtualizaAdmValidator();
    }

    [Test]
    public void Validate_WhenAllFieldsValid_ShouldReturnTrue()
    {
        // Arrange
        var dto = new AtualizaAdmInputDto
        {
            Id = Guid.NewGuid(),
            Nome = "Admin Silva",
            Email = "admin@email.com",
            Telefone = "11999999999"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Empty);
    }

    [Test]
    public void Validate_WhenIdIsEmpty_ShouldReturnError()
    {
        // Arrange
        var dto = new AtualizaAdmInputDto
        {
            Id = Guid.Empty,
            Nome = "Admin Silva",
            Email = "admin@email.com",
            Telefone = "11999999999"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O ID é obrigatório."), Is.True);
    }

    [Test]
    public void Validate_WhenNomeIsEmpty_ShouldReturnError()
    {
        // Arrange
        var dto = new AtualizaAdmInputDto
        {
            Id = Guid.NewGuid(),
            Nome = "",
            Email = "admin@email.com",
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
        var dto = new AtualizaAdmInputDto
        {
            Id = Guid.NewGuid(),
            Nome = new string('A', 101), // 101 characters
            Email = "admin@email.com",
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
        var dto = new AtualizaAdmInputDto
        {
            Id = Guid.NewGuid(),
            Nome = "Admin Silva",
            Email = "",
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
        var dto = new AtualizaAdmInputDto
        {
            Id = Guid.NewGuid(),
            Nome = "Admin Silva",
            Email = "email_invalido",
            Telefone = "11999999999"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O email deve ser um endereço válido."), Is.True);
    }

    [Test]
    public void Validate_WhenTelefoneIsEmpty_ShouldReturnError()
    {
        // Arrange
        var dto = new AtualizaAdmInputDto
        {
            Id = Guid.NewGuid(),
            Nome = "Admin Silva",
            Email = "admin@email.com",
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
        var dto = new AtualizaAdmInputDto
        {
            Id = Guid.NewGuid(),
            Nome = "Admin Silva",
            Email = "admin@email.com",
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
        var dto = new AtualizaAdmInputDto
        {
            Id = Guid.Empty,
            Nome = "",
            Email = "",
            Telefone = ""
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Count.GreaterThanOrEqualTo(4));
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O ID é obrigatório."), Is.True);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O nome é obrigatório."), Is.True);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O email é obrigatório."), Is.True);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O telefone é obrigatório."), Is.True);
    }

    [Test]
    public void Validate_WhenAllFieldsAreMinimumValid_ShouldReturnTrue()
    {
        // Arrange
        var dto = new AtualizaAdmInputDto
        {
            Id = Guid.NewGuid(),
            Nome = "A", // Minimum valid length
            Email = "a@b.co", // Minimum valid email
            Telefone = "1" // Minimum valid length
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Empty);
    }
}
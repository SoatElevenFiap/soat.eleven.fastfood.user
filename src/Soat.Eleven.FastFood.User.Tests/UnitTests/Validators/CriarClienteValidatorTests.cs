using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.Validators;

namespace Soat.Eleven.FastFood.User.Tests.UnitTests.Validators;

[TestFixture]
public class CriarClienteValidatorTests
{
    private CriarClienteValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new CriarClienteValidator();
    }

    [Test]
    public void Validate_WhenAllFieldsValid_ShouldReturnTrue()
    {
        // Arrange
        var dto = new CriarClienteInputDto
        {
            Nome = "João Silva",
            Cpf = "12345678901",
            Email = "joao@email.com",
            Senha = "123456",
            Telefone = "11999999999",
            DataDeNascimento = DateTime.Now.AddYears(-25)
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
        var dto = new CriarClienteInputDto
        {
            Nome = "",
            Cpf = "12345678901",
            Email = "joao@email.com",
            Senha = "123456",
            Telefone = "11999999999",
            DataDeNascimento = DateTime.Now.AddYears(-25)
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
        var dto = new CriarClienteInputDto
        {
            Nome = new string('A', 101), // 101 characters
            Cpf = "12345678901",
            Email = "joao@email.com",
            Senha = "123456",
            Telefone = "11999999999",
            DataDeNascimento = DateTime.Now.AddYears(-25)
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O nome deve ter no máximo 100 caracteres."), Is.True);
    }

    [Test]
    public void Validate_WhenCpfIsEmpty_ShouldReturnError()
    {
        // Arrange
        var dto = new CriarClienteInputDto
        {
            Nome = "João Silva",
            Cpf = "",
            Email = "joao@email.com",
            Senha = "123456",
            Telefone = "11999999999",
            DataDeNascimento = DateTime.Now.AddYears(-25)
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O CPF é obrigatório."), Is.True);
    }

    [Test]
    public void Validate_WhenCpfHasIncorrectLength_ShouldReturnError()
    {
        // Arrange
        var dto = new CriarClienteInputDto
        {
            Nome = "João Silva",
            Cpf = "123456789", // 9 characters instead of 11
            Email = "joao@email.com",
            Senha = "123456",
            Telefone = "11999999999",
            DataDeNascimento = DateTime.Now.AddYears(-25)
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O CPF deve ter exatamente 11 caracteres."), Is.True);
    }

    [Test]
    public void Validate_WhenEmailIsEmpty_ShouldReturnError()
    {
        // Arrange
        var dto = new CriarClienteInputDto
        {
            Nome = "João Silva",
            Cpf = "12345678901",
            Email = "",
            Senha = "123456",
            Telefone = "11999999999",
            DataDeNascimento = DateTime.Now.AddYears(-25)
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
        var dto = new CriarClienteInputDto
        {
            Nome = "João Silva",
            Cpf = "12345678901",
            Email = "email_invalido",
            Senha = "123456",
            Telefone = "11999999999",
            DataDeNascimento = DateTime.Now.AddYears(-25)
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
        var dto = new CriarClienteInputDto
        {
            Nome = "João Silva",
            Cpf = "12345678901",
            Email = "joao@email.com",
            Senha = "",
            Telefone = "11999999999",
            DataDeNascimento = DateTime.Now.AddYears(-25)
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
        var dto = new CriarClienteInputDto
        {
            Nome = "João Silva",
            Cpf = "12345678901",
            Email = "joao@email.com",
            Senha = "123", // Less than 6 characters
            Telefone = "11999999999",
            DataDeNascimento = DateTime.Now.AddYears(-25)
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
        var dto = new CriarClienteInputDto
        {
            Nome = "João Silva",
            Cpf = "12345678901",
            Email = "joao@email.com",
            Senha = "123456",
            Telefone = "",
            DataDeNascimento = DateTime.Now.AddYears(-25)
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
        var dto = new CriarClienteInputDto
        {
            Nome = "João Silva",
            Cpf = "12345678901",
            Email = "joao@email.com",
            Senha = "123456",
            Telefone = "1234567890123456", // 16 characters
            DataDeNascimento = DateTime.Now.AddYears(-25)
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O telefone deve ter no máximo 15 caracteres."), Is.True);
    }

    [Test]
    public void Validate_WhenDataDeNascimentoIsEmpty_ShouldReturnError()
    {
        // Arrange
        var dto = new CriarClienteInputDto
        {
            Nome = "João Silva",
            Cpf = "12345678901",
            Email = "joao@email.com",
            Senha = "123456",
            Telefone = "11999999999",
            DataDeNascimento = default(DateTime)
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "A data de nascimento é obrigatória."), Is.True);
    }

    [Test]
    public void Validate_WhenDataDeNascimentoIsInFuture_ShouldReturnError()
    {
        // Arrange
        var dto = new CriarClienteInputDto
        {
            Nome = "João Silva",
            Cpf = "12345678901",
            Email = "joao@email.com",
            Senha = "123456",
            Telefone = "11999999999",
            DataDeNascimento = DateTime.Now.AddYears(1) // Future date
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "A data de nascimento deve ser anterior à data atual."), Is.True);
    }

    [Test]
    public void Validate_WhenMultipleFieldsAreInvalid_ShouldReturnMultipleErrors()
    {
        // Arrange
        var dto = new CriarClienteInputDto
        {
            Nome = "",
            Cpf = "",
            Email = "",
            Senha = "",
            Telefone = "",
            DataDeNascimento = default(DateTime)
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Count.GreaterThanOrEqualTo(6));
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O nome é obrigatório."), Is.True);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O CPF é obrigatório."), Is.True);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O email é obrigatório."), Is.True);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "A senha é obrigatória."), Is.True);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O telefone é obrigatório."), Is.True);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "A data de nascimento é obrigatória."), Is.True);
    }
}
using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.Validators;

namespace Soat.Eleven.FastFood.User.Tests.UnitTests.Validators;

[TestFixture]
public class AtualizaClienteValidatorTests
{
    private AtualizaClienteValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new AtualizaClienteValidator();
    }

    [Test]
    public void Validate_WhenAllFieldsValid_ShouldReturnTrue()
    {
        // Arrange
        var dto = new AtualizaClienteInputDto
        {
            Id = Guid.NewGuid(),
            Nome = "João Silva",
            Email = "joao@email.com",
            Telefone = "11999999999",
            ClienteId = Guid.NewGuid(),
            Cpf = "12345678901",
            DataDeNascimento = DateTime.Now.AddYears(-25)
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
        var dto = new AtualizaClienteInputDto
        {
            Id = Guid.Empty,
            Nome = "João Silva",
            Email = "joao@email.com",
            Telefone = "11999999999",
            ClienteId = Guid.NewGuid(),
            Cpf = "12345678901",
            DataDeNascimento = DateTime.Now.AddYears(-25)
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
        var dto = new AtualizaClienteInputDto
        {
            Id = Guid.NewGuid(),
            Nome = "",
            Email = "joao@email.com",
            Telefone = "11999999999",
            ClienteId = Guid.NewGuid(),
            Cpf = "12345678901",
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
        var dto = new AtualizaClienteInputDto
        {
            Id = Guid.NewGuid(),
            Nome = new string('A', 101),
            Email = "joao@email.com",
            Telefone = "11999999999",
            ClienteId = Guid.NewGuid(),
            Cpf = "12345678901",
            DataDeNascimento = DateTime.Now.AddYears(-25)
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
        var dto = new AtualizaClienteInputDto
        {
            Id = Guid.NewGuid(),
            Nome = "João Silva",
            Email = "",
            Telefone = "11999999999",
            ClienteId = Guid.NewGuid(),
            Cpf = "12345678901",
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
        var dto = new AtualizaClienteInputDto
        {
            Id = Guid.NewGuid(),
            Nome = "João Silva",
            Email = "email_invalido",
            Telefone = "11999999999",
            ClienteId = Guid.NewGuid(),
            Cpf = "12345678901",
            DataDeNascimento = DateTime.Now.AddYears(-25)
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
        var dto = new AtualizaClienteInputDto
        {
            Id = Guid.NewGuid(),
            Nome = "João Silva",
            Email = "joao@email.com",
            Telefone = "",
            ClienteId = Guid.NewGuid(),
            Cpf = "12345678901",
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
        var dto = new AtualizaClienteInputDto
        {
            Id = Guid.NewGuid(),
            Nome = "João Silva",
            Email = "joao@email.com",
            Telefone = "1234567890123456",
            ClienteId = Guid.NewGuid(),
            Cpf = "12345678901",
            DataDeNascimento = DateTime.Now.AddYears(-25)
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O telefone deve ter no máximo 15 caracteres."), Is.True);
    }

    [Test]
    public void Validate_WhenClienteIdIsEmpty_ShouldReturnError()
    {
        // Arrange
        var dto = new AtualizaClienteInputDto
        {
            Id = Guid.NewGuid(),
            Nome = "João Silva",
            Email = "joao@email.com",
            Telefone = "11999999999",
            ClienteId = Guid.Empty,
            Cpf = "12345678901",
            DataDeNascimento = DateTime.Now.AddYears(-25)
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O ID do cliente é obrigatório."), Is.True);
    }

    [Test]
    public void Validate_WhenCpfIsEmpty_ShouldReturnError()
    {
        // Arrange
        var dto = new AtualizaClienteInputDto
        {
            Id = Guid.NewGuid(),
            Nome = "João Silva",
            Email = "joao@email.com",
            Telefone = "11999999999",
            ClienteId = Guid.NewGuid(),
            Cpf = "",
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
        var dto = new AtualizaClienteInputDto
        {
            Id = Guid.NewGuid(),
            Nome = "João Silva",
            Email = "joao@email.com",
            Telefone = "11999999999",
            ClienteId = Guid.NewGuid(),
            Cpf = "123456789",
            DataDeNascimento = DateTime.Now.AddYears(-25)
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "O CPF deve ter exatamente 11 caracteres."), Is.True);
    }

    [Test]
    public void Validate_WhenDataDeNascimentoIsEmpty_ShouldReturnError()
    {
        // Arrange
        var dto = new AtualizaClienteInputDto
        {
            Id = Guid.NewGuid(),
            Nome = "João Silva",
            Email = "joao@email.com",
            Telefone = "11999999999",
            ClienteId = Guid.NewGuid(),
            Cpf = "12345678901",
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
        var dto = new AtualizaClienteInputDto
        {
            Id = Guid.NewGuid(),
            Nome = "João Silva",
            Email = "joao@email.com",
            Telefone = "11999999999",
            ClienteId = Guid.NewGuid(),
            Cpf = "12345678901",
            DataDeNascimento = DateTime.Now.AddYears(1)
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(x => x.ErrorMessage == "A data de nascimento deve ser anterior à data atual."), Is.True);
    }
}
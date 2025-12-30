using FluentValidation;
using Soat.Eleven.FastFood.User.Application.Validators;
using Soat.Eleven.FastFood.User.Domain.ErrorValidators;

namespace Soat.Eleven.FastFood.User.Tests.UnitTests.Validators;

[TestFixture]
public class BaseValidatorTests
{
    // Classe de teste concreta para testar BaseValidator
    private class TestDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public string Cpf { get; set; }
        public DateTime DataDeNascimento { get; set; }
    }

    // Validator concreto para testes de ID
    private class TestIdValidator : BaseValidator<TestDto>
    {
        public TestIdValidator()
        {
            ApplyIdRules(RuleFor(x => x.Id));
        }
    }

    // Validator concreto para testes de Nome
    private class TestNomeValidator : BaseValidator<TestDto>
    {
        public TestNomeValidator()
        {
            ApplyNomeRules(RuleFor(x => x.Nome));
        }
    }

    // Validator concreto para testes de Email
    private class TestEmailValidator : BaseValidator<TestDto>
    {
        public TestEmailValidator()
        {
            ApplyEmailRules(RuleFor(x => x.Email));
        }
    }

    // Validator concreto para testes de Telefone
    private class TestTelefoneValidator : BaseValidator<TestDto>
    {
        public TestTelefoneValidator()
        {
            ApplyTelefoneRules(RuleFor(x => x.Telefone));
        }
    }

    // Validator concreto para testes de Senha
    private class TestSenhaValidator : BaseValidator<TestDto>
    {
        public TestSenhaValidator()
        {
            ApplySenhaRules(RuleFor(x => x.Senha));
        }
    }

    // Validator concreto para testes de CPF
    private class TestCpfValidator : BaseValidator<TestDto>
    {
        public TestCpfValidator()
        {
            ApplyCpfRules(RuleFor(x => x.Cpf));
        }
    }

    // Validator concreto para testes de Data de Nascimento
    private class TestDataDeNascimentoValidator : BaseValidator<TestDto>
    {
        public TestDataDeNascimentoValidator()
        {
            ApplyDataDeNascimentoRules(RuleFor(x => x.DataDeNascimento));
        }
    }

    #region ApplyIdRules Tests

    [Test]
    public void ApplyIdRules_WhenIdIsValid_ShouldReturnTrue()
    {
        // Arrange
        var validator = new TestIdValidator();
        var dto = new TestDto { Id = Guid.NewGuid() };

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.True);
            Assert.That(result.Errors, Is.Empty);
        }
    }

    [Test]
    public void ApplyIdRules_WhenIdIsEmpty_ShouldReturnError()
    {
        // Arrange
        var validator = new TestIdValidator();
        var dto = new TestDto { Id = Guid.Empty };

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(x => x.ErrorMessage == ErrorMessages.ID_REQUIRED), Is.True);
        }
    }

    #endregion

    #region ApplyNomeRules Tests

    [Test]
    public void ApplyNomeRules_WhenNomeIsValid_ShouldReturnTrue()
    {
        // Arrange
        var validator = new TestNomeValidator();
        var dto = new TestDto { Nome = "João Silva" };

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.True);
            Assert.That(result.Errors, Is.Empty);
        }
    }

    [Test]
    public void ApplyNomeRules_WhenNomeIsEmpty_ShouldReturnError()
    {
        // Arrange
        var validator = new TestNomeValidator();
        var dto = new TestDto { Nome = "" };

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(x => x.ErrorMessage == ErrorMessages.NAME_REQUIRED), Is.True);
        }
    }

    [Test]
    public void ApplyNomeRules_WhenNomeIsNull_ShouldReturnError()
    {
        // Arrange
        var validator = new TestNomeValidator();
        var dto = new TestDto();

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(x => x.ErrorMessage == ErrorMessages.NAME_REQUIRED), Is.True);
        }
    }

    [Test]
    public void ApplyNomeRules_WhenNomeExceedsMaxLength_ShouldReturnError()
    {
        // Arrange
        var validator = new TestNomeValidator();
        var dto = new TestDto { Nome = new string('A', 101) };

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(x => x.ErrorMessage == ErrorMessages.NAME_MAX_LENGTH), Is.True);
        }
    }

    [Test]
    public void ApplyNomeRules_WhenNomeIsExactlyMaxLength_ShouldReturnTrue()
    {
        // Arrange
        var validator = new TestNomeValidator();
        var dto = new TestDto { Nome = new string('A', 100) };

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.True);
            Assert.That(result.Errors, Is.Empty);
        }
    }

    #endregion

    #region ApplyEmailRules Tests

    [Test]
    public void ApplyEmailRules_WhenEmailIsValid_ShouldReturnTrue()
    {
        // Arrange
        var validator = new TestEmailValidator();
        var dto = new TestDto { Email = "usuario@email.com" };

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.True);
            Assert.That(result.Errors, Is.Empty);
        }
    }

    [Test]
    public void ApplyEmailRules_WhenEmailIsEmpty_ShouldReturnError()
    {
        // Arrange
        var validator = new TestEmailValidator();
        var dto = new TestDto { Email = "" };

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(x => x.ErrorMessage == ErrorMessages.EMAIL_REQUIRED), Is.True);
        }
    }

    [Test]
    public void ApplyEmailRules_WhenEmailIsNull_ShouldReturnError()
    {
        // Arrange
        var validator = new TestEmailValidator();
        var dto = new TestDto();

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(x => x.ErrorMessage == ErrorMessages.EMAIL_REQUIRED), Is.True);
        }
    }

    [Test]
    public void ApplyEmailRules_WhenEmailIsInvalid_ShouldReturnError()
    {
        // Arrange
        var validator = new TestEmailValidator();
        var dto = new TestDto { Email = "email_invalido" };

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(x => x.ErrorMessage == ErrorMessages.EMAIL_INVALID), Is.True);
        }
    }

    [Test]
    public void ApplyEmailRules_WhenEmailMissingAtSign_ShouldReturnError()
    {
        // Arrange
        var validator = new TestEmailValidator();
        var dto = new TestDto { Email = "emailsemarroba.com" };

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(x => x.ErrorMessage == ErrorMessages.EMAIL_INVALID), Is.True);
        }
    }

    [Test]
    public void ApplyEmailRules_WhenEmailMissingDomain_ShouldReturnError()
    {
        // Arrange
        var validator = new TestEmailValidator();
        var dto = new TestDto { Email = "email@" };

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(x => x.ErrorMessage == ErrorMessages.EMAIL_INVALID), Is.True);
        }
    }

    #endregion

    #region ApplyTelefoneRules Tests

    [Test]
    public void ApplyTelefoneRules_WhenTelefoneIsValid_ShouldReturnTrue()
    {
        // Arrange
        var validator = new TestTelefoneValidator();
        var dto = new TestDto { Telefone = "11999999999" };

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.True);
            Assert.That(result.Errors, Is.Empty);
        }
    }

    [Test]
    public void ApplyTelefoneRules_WhenTelefoneIsEmpty_ShouldReturnError()
    {
        // Arrange
        var validator = new TestTelefoneValidator();
        var dto = new TestDto { Telefone = "" };

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(x => x.ErrorMessage == ErrorMessages.PHONE_REQUIRED), Is.True);
        }
    }

    [Test]
    public void ApplyTelefoneRules_WhenTelefoneIsNull_ShouldReturnError()
    {
        // Arrange
        var validator = new TestTelefoneValidator();
        var dto = new TestDto();

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(x => x.ErrorMessage == ErrorMessages.PHONE_REQUIRED), Is.True);
        }
    }

    [Test]
    public void ApplyTelefoneRules_WhenTelefoneExceedsMaxLength_ShouldReturnError()
    {
        // Arrange
        var validator = new TestTelefoneValidator();
        var dto = new TestDto { Telefone = "1234567890123456" }; // 16 characters

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(x => x.ErrorMessage == ErrorMessages.PHONE_MAX_LENGTH), Is.True);
        }
    }

    [Test]
    public void ApplyTelefoneRules_WhenTelefoneIsExactlyMaxLength_ShouldReturnTrue()
    {
        // Arrange
        var validator = new TestTelefoneValidator();
        var dto = new TestDto { Telefone = "123456789012345" }; // 15 characters

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.True);
            Assert.That(result.Errors, Is.Empty);
        }
    }

    #endregion

    #region ApplySenhaRules Tests

    [Test]
    public void ApplySenhaRules_WhenSenhaIsValid_ShouldReturnTrue()
    {
        // Arrange
        var validator = new TestSenhaValidator();
        var dto = new TestDto { Senha = "123456" };

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.True);
            Assert.That(result.Errors, Is.Empty);
        }
    }

    [Test]
    public void ApplySenhaRules_WhenSenhaIsEmpty_ShouldReturnError()
    {
        // Arrange
        var validator = new TestSenhaValidator();
        var dto = new TestDto { Senha = "" };

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(x => x.ErrorMessage == ErrorMessages.PASSWORD_REQUIRED), Is.True);
        }
    }

    [Test]
    public void ApplySenhaRules_WhenSenhaIsNull_ShouldReturnError()
    {
        // Arrange
        var validator = new TestSenhaValidator();
        var dto = new TestDto();

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(x => x.ErrorMessage == ErrorMessages.PASSWORD_REQUIRED), Is.True);
        }
    }

    [Test]
    public void ApplySenhaRules_WhenSenhaBelowMinLength_ShouldReturnError()
    {
        // Arrange
        var validator = new TestSenhaValidator();
        var dto = new TestDto { Senha = "12345" }; // 5 characters

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(x => x.ErrorMessage == ErrorMessages.PASSWORD_MIN_LENGTH), Is.True);
        }
    }

    [Test]
    public void ApplySenhaRules_WhenSenhaIsExactlyMinLength_ShouldReturnTrue()
    {
        // Arrange
        var validator = new TestSenhaValidator();
        var dto = new TestDto { Senha = "123456" }; // 6 characters

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.True);
            Assert.That(result.Errors, Is.Empty);
        }
    }

    #endregion

    #region ApplyCpfRules Tests

    [Test]
    public void ApplyCpfRules_WhenCpfIsValid_ShouldReturnTrue()
    {
        // Arrange
        var validator = new TestCpfValidator();
        var dto = new TestDto { Cpf = "12345678901" };

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.True);
            Assert.That(result.Errors, Is.Empty);
        }
    }

    [Test]
    public void ApplyCpfRules_WhenCpfIsEmpty_ShouldReturnError()
    {
        // Arrange
        var validator = new TestCpfValidator();
        var dto = new TestDto { Cpf = "" };

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(x => x.ErrorMessage == ErrorMessages.CPF_REQUIRED), Is.True);
        }
    }

    [Test]
    public void ApplyCpfRules_WhenCpfIsNull_ShouldReturnError()
    {
        // Arrange
        var validator = new TestCpfValidator();
        var dto = new TestDto();

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(x => x.ErrorMessage == ErrorMessages.CPF_REQUIRED), Is.True);
        }
    }

    [Test]
    public void ApplyCpfRules_WhenCpfHasIncorrectLength_ShouldReturnError()
    {
        // Arrange
        var validator = new TestCpfValidator();
        var dto = new TestDto { Cpf = "123456789" }; // 9 characters

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(x => x.ErrorMessage == ErrorMessages.CPF_LENGTH), Is.True);
        }
    }

    [Test]
    public void ApplyCpfRules_WhenCpfExceedsCorrectLength_ShouldReturnError()
    {
        // Arrange
        var validator = new TestCpfValidator();
        var dto = new TestDto { Cpf = "123456789012" }; // 12 characters

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(x => x.ErrorMessage == ErrorMessages.CPF_LENGTH), Is.True);
        }
    }

    #endregion

    #region ApplyDataDeNascimentoRules Tests

    [Test]
    public void ApplyDataDeNascimentoRules_WhenDateIsValid_ShouldReturnTrue()
    {
        // Arrange
        var validator = new TestDataDeNascimentoValidator();
        var dto = new TestDto { DataDeNascimento = DateTime.Now.AddYears(-25) };

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.True);
            Assert.That(result.Errors, Is.Empty);
        }
    }

    [Test]
    public void ApplyDataDeNascimentoRules_WhenDateIsEmpty_ShouldReturnError()
    {
        // Arrange
        var validator = new TestDataDeNascimentoValidator();
        var dto = new TestDto { DataDeNascimento = default };

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(x => x.ErrorMessage == ErrorMessages.BIRTHDATE_REQUIRED), Is.True);
        }
    }

    [Test]
    public void ApplyDataDeNascimentoRules_WhenDateIsInFuture_ShouldReturnError()
    {
        // Arrange
        var validator = new TestDataDeNascimentoValidator();
        var dto = new TestDto { DataDeNascimento = DateTime.Now.AddYears(1) };

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(x => x.ErrorMessage == ErrorMessages.BIRTHDATE_INVALID), Is.True);
        }
    }

    [Test]
    public void ApplyDataDeNascimentoRules_WhenDateIsToday_ShouldReturnError()
    {
        // Arrange
        var validator = new TestDataDeNascimentoValidator();
        var dto = new TestDto { DataDeNascimento = DateTime.Now };

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(x => x.ErrorMessage == ErrorMessages.BIRTHDATE_INVALID), Is.True);
        }
    }

    [Test]
    public void ApplyDataDeNascimentoRules_WhenDateIsYesterday_ShouldReturnTrue()
    {
        // Arrange
        var validator = new TestDataDeNascimentoValidator();
        var dto = new TestDto { DataDeNascimento = DateTime.Now.AddDays(-1) };

        // Act
        var result = validator.Validate(dto);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsValid, Is.True);
            Assert.That(result.Errors, Is.Empty);
        }
    }

    #endregion
}

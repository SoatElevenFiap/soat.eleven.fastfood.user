using FluentValidation;
using FluentValidation.Results;
using Moq;
using Soat.Eleven.FastFood.User.Application.Handlers;
using NUnit.Framework;

namespace Soat.Eleven.FastFood.User.Tests.UnitTests.Handler;

[TestFixture]
public class BaseHandlerTests
{
    // DTO de teste
    private class TestInputDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
    }

    // Validator de teste
    private class TestInputValidator : AbstractValidator<TestInputDto>
    {
        public TestInputValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome é obrigatório");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email é obrigatório")
                                 .EmailAddress().WithMessage("Email deve ser válido");
        }
    }

    // Handler concreto para testes
    private class TestHandler : BaseHandler
    {
        public bool TestValidate<T>(IValidator<T> validator, T input)
        {
            return Validate(validator, input);
        }

        public ResponseHandler TestSendError()
        {
            return SendError();
        }

        public void TestAddError(string error)
        {
            AddError(error);
        }

        public ResponseHandler TestSendErrorWithMessage(string message)
        {
            return SendError(message);
        }

        public ResponseHandler TestSendSuccess(object? data)
        {
            return SendSuccess(data);
        }

        public ResponseHandler TestSend(object data)
        {
            return Send(data);
        }
    }

    private TestHandler _handler;
    private TestInputValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _handler = new TestHandler();
        _validator = new TestInputValidator();
    }

    #region Validate Tests

    [Test]
    public void Validate_WhenInputIsValid_ShouldReturnFalse()
    {
        // Arrange
        var input = new TestInputDto
        {
            Nome = "João Silva",
            Email = "joao@email.com"
        };

        // Act
        var hasErrors = _handler.TestValidate(_validator, input);

        // Assert
        Assert.That(hasErrors, Is.False);
    }

    [Test]
    public void Validate_WhenInputHasOneError_ShouldReturnTrue()
    {
        // Arrange
        var input = new TestInputDto
        {
            Nome = "",
            Email = "joao@email.com"
        };

        // Act
        var hasErrors = _handler.TestValidate(_validator, input);

        // Assert
        Assert.That(hasErrors, Is.True);
    }

    [Test]
    public void Validate_WhenInputHasMultipleErrors_ShouldReturnTrue()
    {
        // Arrange
        var input = new TestInputDto
        {
            Nome = "",
            Email = ""
        };

        // Act
        var hasErrors = _handler.TestValidate(_validator, input);

        // Assert
        Assert.That(hasErrors, Is.True);
    }

    [Test]
    public void Validate_WhenInputHasInvalidEmail_ShouldReturnTrue()
    {
        // Arrange
        var input = new TestInputDto
        {
            Nome = "João Silva",
            Email = "email-invalido"
        };

        // Act
        var hasErrors = _handler.TestValidate(_validator, input);

        // Assert
        Assert.That(hasErrors, Is.True);
    }

    [Test]
    public void Validate_WhenCalledMultipleTimes_ShouldAccumulateErrors()
    {
        // Arrange
        var input1 = new TestInputDto { Nome = "", Email = "joao@email.com" };
        var input2 = new TestInputDto { Nome = "João", Email = "" };

        // Act
        _handler.TestValidate(_validator, input1);
        _handler.TestValidate(_validator, input2);
        var response = _handler.TestSendError();

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(response.Success, Is.False);
            Assert.That(response.Data, Is.TypeOf<List<string>>());
        }
        
        var errors = (List<string>)response.Data;
        Assert.That(errors.Count, Is.GreaterThanOrEqualTo(2));
    }

    #endregion

    #region SendError Tests

    [Test]
    public void SendError_WhenCalledWithoutErrors_ShouldReturnEmptyErrorList()
    {
        // Act
        var response = _handler.TestSendError();

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.False);
            Assert.That(response.Data, Is.TypeOf<List<string>>());
        }
        
        var errors = (List<string>)response.Data;
        Assert.That(errors, Is.Empty);
    }

    [Test]
    public void SendError_WhenCalledAfterValidation_ShouldReturnValidationErrors()
    {
        // Arrange
        var input = new TestInputDto { Nome = "", Email = "" };
        _handler.TestValidate(_validator, input);

        // Act
        var response = _handler.TestSendError();

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.False);
            Assert.That(response.Data, Is.TypeOf<List<string>>());
        }
        
        var errors = (List<string>)response.Data;
        Assert.That(errors, Is.Not.Empty);
    }

    [Test]
    public void SendError_WithMessage_ShouldReturnErrorWithMessage()
    {
        // Arrange
        var errorMessage = "Erro customizado";

        // Act
        var response = _handler.TestSendErrorWithMessage(errorMessage);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.False);
            Assert.That(response.Data, Is.EqualTo(errorMessage));
        }
    }

    [Test]
    public void SendError_WithEmptyMessage_ShouldReturnErrorWithEmptyString()
    {
        // Arrange
        var errorMessage = "";

        // Act
        var response = _handler.TestSendErrorWithMessage(errorMessage);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.False);
            Assert.That(response.Data, Is.EqualTo(errorMessage));
        }
    }

    #endregion

    #region AddError Tests

    [Test]
    public void AddError_WhenCalledOnce_ShouldAddSingleError()
    {
        // Arrange
        var errorMessage = "Erro adicionado manualmente";

        // Act
        _handler.TestAddError(errorMessage);
        var response = _handler.TestSendError();

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(response.Success, Is.False);
            Assert.That(response.Data, Is.TypeOf<List<string>>());
        }
        
        var errors = (List<string>)response.Data;
        using (Assert.EnterMultipleScope())
        {
            Assert.That(errors.Count, Is.EqualTo(1));
            Assert.That(errors[0], Is.EqualTo(errorMessage));
        }
    }

    [Test]
    public void AddError_WhenCalledMultipleTimes_ShouldAddMultipleErrors()
    {
        // Arrange
        var error1 = "Primeiro erro";
        var error2 = "Segundo erro";
        var error3 = "Terceiro erro";

        // Act
        _handler.TestAddError(error1);
        _handler.TestAddError(error2);
        _handler.TestAddError(error3);
        var response = _handler.TestSendError();

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(response.Success, Is.False);
            Assert.That(response.Data, Is.TypeOf<List<string>>());
        }
        
        var errors = (List<string>)response.Data;
        using (Assert.EnterMultipleScope())
        {
            Assert.That(errors.Count, Is.EqualTo(3));
            Assert.That(errors[0], Is.EqualTo(error1));
            Assert.That(errors[1], Is.EqualTo(error2));
            Assert.That(errors[2], Is.EqualTo(error3));
        }
    }

    #endregion

    #region SendSuccess Tests

    [Test]
    public void SendSuccess_WithValidData_ShouldReturnSuccessResponse()
    {
        // Arrange
        var data = new { Id = 1, Nome = "Teste" };

        // Act
        var response = _handler.TestSendSuccess(data);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.True);
            Assert.That(response.Data, Is.EqualTo(data));
        }
    }

    [Test]
    public void SendSuccess_WithNull_ShouldReturnSuccessResponseWithNull()
    {
        // Act
        var response = _handler.TestSendSuccess(null);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.True);
            Assert.That(response.Data, Is.Null);
        }
    }

    [Test]
    public void SendSuccess_WithString_ShouldReturnSuccessResponseWithString()
    {
        // Arrange
        var data = "Operação realizada com sucesso";

        // Act
        var response = _handler.TestSendSuccess(data);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.True);
            Assert.That(response.Data, Is.EqualTo(data));
        }
    }

    [Test]
    public void SendSuccess_WithList_ShouldReturnSuccessResponseWithList()
    {
        // Arrange
        var data = new List<string> { "Item 1", "Item 2", "Item 3" };

        // Act
        var response = _handler.TestSendSuccess(data);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.True);
            Assert.That(response.Data, Is.EqualTo(data));
        }
    }

    [Test]
    public void SendSuccess_WithBoolean_ShouldReturnSuccessResponseWithBoolean()
    {
        // Arrange
        var data = true;

        // Act
        var response = _handler.TestSendSuccess(data);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.True);
            Assert.That(response.Data, Is.EqualTo(data));
        }
    }

    #endregion

    #region Send Tests

    [Test]
    public void Send_WithNonNullData_ShouldReturnSuccessResponse()
    {
        // Arrange
        var data = new { Id = 1, Nome = "Teste" };

        // Act
        var response = _handler.TestSend(data);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.True);
            Assert.That(response.Data, Is.EqualTo(data));
        }
    }

    [Test]
    public void Send_WithEmptyString_ShouldReturnSuccessResponse()
    {
        // Arrange
        var data = "";

        // Act
        var response = _handler.TestSend(data);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.True);
            Assert.That(response.Data, Is.EqualTo(data));
        }
    }

    [Test]
    public void Send_WithZero_ShouldReturnSuccessResponse()
    {
        // Arrange
        var data = 0;

        // Act
        var response = _handler.TestSend(data);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.True);
            Assert.That(response.Data, Is.EqualTo(data));
        }
    }

    [Test]
    public void Send_WithEmptyList_ShouldReturnSuccessResponse()
    {
        // Arrange
        var data = new List<string>();

        // Act
        var response = _handler.TestSend(data);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.True);
            Assert.That(response.Data, Is.EqualTo(data));
        }
    }

    #endregion

    #region ResponseHandler Tests

    [Test]
    public void ResponseHandler_WhenCreated_ShouldHaveDefaultValues()
    {
        // Act
        var response = new ResponseHandler();

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.False);
            Assert.That(response.Data, Is.Null);
        }
    }

    [Test]
    public void ResponseHandler_WhenPropertiesSet_ShouldReturnSetValues()
    {
        // Arrange
        var data = new { Id = 1, Nome = "Teste" };

        // Act
        var response = new ResponseHandler
        {
            Success = true,
            Data = data
        };

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(response.Success, Is.True);
            Assert.That(response.Data, Is.EqualTo(data));
        }
    }

    #endregion

    #region Integration Tests

    [Test]
    public void Integration_ValidateAndSendError_ShouldReturnErrorsFromValidation()
    {
        // Arrange
        var input = new TestInputDto
        {
            Nome = "",
            Email = "invalid-email"
        };

        // Act
        _handler.TestValidate(_validator, input);
        var response = _handler.TestSendError();

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(response.Success, Is.False);
            Assert.That(response.Data, Is.TypeOf<List<string>>());
        }
        
        var errors = (List<string>)response.Data;
        using (Assert.EnterMultipleScope())
        {
            Assert.That(errors.Count, Is.GreaterThanOrEqualTo(2));
            Assert.That(errors.Any(e => e.Contains("Nome")), Is.True);
            Assert.That(errors.Any(e => e.Contains("Email")), Is.True);
        }
    }

    [Test]
    public void Integration_AddErrorAndValidate_ShouldCombineErrors()
    {
        // Arrange
        var input = new TestInputDto { Nome = "", Email = "test@email.com" };
        var customError = "Erro customizado";

        // Act
        _handler.TestAddError(customError);
        _handler.TestValidate(_validator, input);
        var response = _handler.TestSendError();

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(response.Success, Is.False);
            Assert.That(response.Data, Is.TypeOf<List<string>>());
        }
        
        var errors = (List<string>)response.Data;
        using (Assert.EnterMultipleScope())
        {
            Assert.That(errors.Count, Is.GreaterThanOrEqualTo(2));
            Assert.That(errors.Contains(customError), Is.True);
            Assert.That(errors.Any(e => e.Contains("Nome")), Is.True);
        }
    }

    [Test]
    public void Integration_ValidateMultipleInputs_ShouldAccumulateAllErrors()
    {
        // Arrange
        var input1 = new TestInputDto { Nome = "", Email = "test@email.com" };
        var input2 = new TestInputDto { Nome = "João", Email = "" };
        var input3 = new TestInputDto { Nome = "", Email = "" };

        // Act
        _handler.TestValidate(_validator, input1);
        _handler.TestValidate(_validator, input2);
        _handler.TestValidate(_validator, input3);
        var response = _handler.TestSendError();

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(response.Success, Is.False);
            Assert.That(response.Data, Is.TypeOf<List<string>>());
        }
        
        var errors = (List<string>)response.Data;
        Assert.That(errors.Count, Is.GreaterThanOrEqualTo(4));
    }

    #endregion
}

using FluentValidation;

namespace Soat.Eleven.FastFood.User.Application.Handlers;

public abstract class BaseHandler
{
    private readonly List<string> validateErrors = new();
    protected bool Validate<T>(IValidator<T> validator, T input)
    {
        var validationResult = validator.Validate(input);

        var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        validateErrors.AddRange(errors);

        return !validationResult.IsValid || validateErrors.Count > 0;
    }

    protected ResponseHandler SendError()
    {
        var response = new ResponseHandler();
        response.Success = false;
        response.Data = validateErrors;
        return response;
    }

    protected ResponseHandler SendError(string message)
    {
        var response = new ResponseHandler();
        response.Success = false;
        response.Data = message;
        return response;
    }

    protected void AddError(string error)
    {
        validateErrors.Add(error);
    }

    protected ResponseHandler SendSuccess(object data)
    {
        var response = new ResponseHandler();
        response.Success = true;
        response.Data = data;
        return response;
    }

    protected ResponseHandler Send(object data)
    {
        var response = new ResponseHandler();
        response.Success = data is not null;
        response.Data = data;
        return response;
    }
}

public class ResponseHandler
{
    public bool Success { get; set; }
    public object Data { get; set; }
}
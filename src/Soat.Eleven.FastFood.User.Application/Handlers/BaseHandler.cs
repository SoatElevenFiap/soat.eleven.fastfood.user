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

        return validationResult.IsValid;
    }

    protected Response SendError()
    {
        var response = new Response();
        response.Success = false;
        response.Data = validateErrors;
        return response;
    }

    protected Response SendError(string message)
    {
        var response = new Response();
        response.Success = false;
        response.Data = message;
        return response;
    }

    protected void AddError(string error)
    {
        validateErrors.Add(error);
    }

    protected Response SendSuccess(object data)
    {
        var response = new Response();
        response.Success = true;
        response.Data = data;
        return response;
    }
}

public class Response
{
    public bool Success { get; set; }
    public object Data { get; set; }
}
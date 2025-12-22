using FluentValidation;
using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Domain.ErrorValidators;

namespace Soat.Eleven.FastFood.User.Application.Validators;

public class AtualizarSenhaValidator : AbstractValidator<AtualizarSenhaInputDto>
{
    public AtualizarSenhaValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage(ErrorMessages.CURRENT_PASSWORD_REQUIRED);
        
        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage(ErrorMessages.NEW_PASSWORD_REQUIRED)
            .MinimumLength(6).WithMessage(ErrorMessages.NEW_PASSWORD_MIN_LENGTH);
        
        RuleFor(x => x)
            .Must(x => x.NewPassword != x.CurrentPassword)
            .WithMessage(ErrorMessages.NEW_PASSWORD_DIFFERENT);
    }
}
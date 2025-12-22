using FluentValidation;
using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Domain.ErrorValidators;

namespace Soat.Eleven.FastFood.User.Application.Validators;

public class CriarAdmValidator : AbstractValidator<CriarAdmInputDto>
{
    public CriarAdmValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage(ErrorMessages.NAME_REQUIRED)
            .MaximumLength(100).WithMessage(ErrorMessages.NAME_MAX_LENGTH);
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ErrorMessages.EMAIL_REQUIRED)
            .EmailAddress().WithMessage(ErrorMessages.EMAIL_INVALID);
        
        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage(ErrorMessages.PASSWORD_REQUIRED)
            .MinimumLength(6).WithMessage(ErrorMessages.PASSWORD_MIN_LENGTH);
        
        RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage(ErrorMessages.PHONE_REQUIRED)
            .MaximumLength(15).WithMessage(ErrorMessages.PHONE_MAX_LENGTH);
    }
}
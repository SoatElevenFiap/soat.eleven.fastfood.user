using FluentValidation;
using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Domain.ErrorValidators;

namespace Soat.Eleven.FastFood.User.Application.Validators;

public class AtualizaAdmValidator : AbstractValidator<AtualizaAdmInputDto>
{
    public AtualizaAdmValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ErrorMessages.ID_REQUIRED);

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage(ErrorMessages.NAME_REQUIRED)
            .MaximumLength(100).WithMessage(ErrorMessages.NAME_MAX_LENGTH);
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ErrorMessages.EMAIL_REQUIRED)
            .EmailAddress().WithMessage(ErrorMessages.EMAIL_INVALID);

        RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage(ErrorMessages.PHONE_REQUIRED)
            .MaximumLength(15).WithMessage(ErrorMessages.PHONE_MAX_LENGTH);
    }
}
using FluentValidation;
using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Domain.ErrorValidators;

namespace Soat.Eleven.FastFood.User.Application.Validators;

public class CriarAdmValidator : BaseValidator<CriarAdmInputDto>
{
    public CriarAdmValidator()
    {
        ApplyNomeRules(RuleFor(x => x.Nome));

        ApplyEmailRules(RuleFor(x => x.Email));

        ApplyTelefoneRules(RuleFor(x => x.Telefone));

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage(ErrorMessages.PASSWORD_REQUIRED)
            .MinimumLength(6).WithMessage(ErrorMessages.PASSWORD_MIN_LENGTH);
    }
}
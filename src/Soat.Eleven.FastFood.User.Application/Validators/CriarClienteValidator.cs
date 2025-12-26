using FluentValidation;
using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Domain.ErrorValidators;

namespace Soat.Eleven.FastFood.User.Application.Validators;

public class CriarClienteValidator : BaseValidator<CriarClienteInputDto>
{
    public CriarClienteValidator()
    {
        ApplyNomeRules(RuleFor(x => x.Nome));

        ApplyEmailRules(RuleFor(x => x.Email));

        ApplyTelefoneRules(RuleFor(x => x.Telefone));

        ApplyCpfRules(RuleFor(x => x.Cpf));

        ApplyDataDeNascimentoRules(RuleFor(x => x.DataDeNascimento));

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage(ErrorMessages.PASSWORD_REQUIRED)
            .MinimumLength(6).WithMessage(ErrorMessages.PASSWORD_MIN_LENGTH);
    }
}

using FluentValidation;
using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Domain.ErrorValidators;

namespace Soat.Eleven.FastFood.User.Application.Validators;

public class AtualizaClienteValidator : BaseValidator<AtualizaClienteInputDto>
{
    public AtualizaClienteValidator()
    {
        ApplyIdRules(RuleFor(x => x.Id));

        ApplyNomeRules(RuleFor(x => x.Nome));

        ApplyEmailRules(RuleFor(x => x.Email));

        ApplyTelefoneRules(RuleFor(x => x.Telefone));

        RuleFor(x => x.ClienteId)
            .NotEmpty().WithMessage(ErrorMessages.CLIENTE_ID_REQUIRED);
        
        ApplyCpfRules(RuleFor(x => x.Cpf));

        ApplyDataDeNascimentoRules(RuleFor(x => x.DataDeNascimento));
    }
}
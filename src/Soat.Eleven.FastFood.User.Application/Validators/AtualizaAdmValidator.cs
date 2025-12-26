using FluentValidation;
using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Domain.ErrorValidators;

namespace Soat.Eleven.FastFood.User.Application.Validators;

public class AtualizaAdmValidator : BaseValidator<AtualizaAdmInputDto>
{
    public AtualizaAdmValidator()
    {
        ApplyIdRules(RuleFor(x => x.Id));

        ApplyNomeRules(RuleFor(x => x.Nome));

        ApplyEmailRules(RuleFor(x => x.Email));
        
        ApplyTelefoneRules(RuleFor(x => x.Telefone));
    }
}
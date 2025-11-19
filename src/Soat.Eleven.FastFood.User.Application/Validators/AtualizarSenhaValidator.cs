using FluentValidation;
using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;

namespace Soat.Eleven.FastFood.User.Application.Validators;

public class AtualizarSenhaValidator : AbstractValidator<AtualizarSenhaInputDto>
{
    public AtualizarSenhaValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage("A senha atual é obrigatória.");
        
        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("A nova senha é obrigatória.")
            .MinimumLength(6).WithMessage("A nova senha deve ter no mínimo 6 caracteres.");
        
        RuleFor(x => x)
            .Must(x => x.NewPassword != x.CurrentPassword)
            .WithMessage("A nova senha deve ser diferente da senha atual.");
    }
}
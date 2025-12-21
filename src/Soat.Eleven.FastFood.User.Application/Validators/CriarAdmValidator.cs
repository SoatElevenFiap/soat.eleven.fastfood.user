using FluentValidation;
using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;

namespace Soat.Eleven.FastFood.User.Application.Validators;

public class CriarAdmValidator : AbstractValidator<CriarAdmInputDto>
{
    public CriarAdmValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O email é obrigatório.")
            .EmailAddress().WithMessage("O email deve ser um endereço válido.");
        
        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres.");
        
        RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage("O telefone é obrigatório.")
            .MaximumLength(15).WithMessage("O telefone deve ter no máximo 15 caracteres.");
    }
}
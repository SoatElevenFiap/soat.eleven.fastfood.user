using FluentValidation;
using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Domain.ErrorValidators;

namespace Soat.Eleven.FastFood.User.Application.Validators;

public class AtualizaClienteValidator : AbstractValidator<AtualizaClienteInputDto>
{
    public AtualizaClienteValidator()
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
        
        RuleFor(x => x.ClienteId)
            .NotEmpty().WithMessage(ErrorMessages.CLIENTE_ID_REQUIRED);
        
        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage(ErrorMessages.CPF_REQUIRED)
            .Length(11).WithMessage(ErrorMessages.CPF_LENGTH);
        
        RuleFor(x => x.DataDeNascimento)
            .NotEmpty().WithMessage(ErrorMessages.BIRTHDATE_REQUIRED)
            .LessThan(DateTime.Now).WithMessage(ErrorMessages.BIRTHDATE_INVALID);
    }
}
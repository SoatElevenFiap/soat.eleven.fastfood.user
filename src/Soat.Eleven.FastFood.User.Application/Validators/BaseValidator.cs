using FluentValidation;
using Soat.Eleven.FastFood.User.Domain.ErrorValidators;

namespace Soat.Eleven.FastFood.User.Application.Validators;

/// <summary>
/// Classe base com regras de validação compartilhadas
/// </summary>
public abstract class BaseValidator<T> : AbstractValidator<T>
{
    /// <summary>
    /// Aplica regras de validação para o campo Id
    /// </summary>
    protected void ApplyIdRules(IRuleBuilder<T, Guid> ruleBuilder)
    {
        ruleBuilder
            .NotEmpty().WithMessage(ErrorMessages.ID_REQUIRED);
    }

    /// <summary>
    /// Aplica regras de validação para o campo Nome
    /// </summary>
    protected void ApplyNomeRules(IRuleBuilder<T, string> ruleBuilder)
    {
        ruleBuilder
            .NotEmpty().WithMessage(ErrorMessages.NAME_REQUIRED)
            .MaximumLength(100).WithMessage(ErrorMessages.NAME_MAX_LENGTH);
    }

    /// <summary>
    /// Aplica regras de validação para o campo Email
    /// </summary>
    protected void ApplyEmailRules(IRuleBuilder<T, string> ruleBuilder)
    {
        ruleBuilder
            .NotEmpty().WithMessage(ErrorMessages.EMAIL_REQUIRED)
            .EmailAddress().WithMessage(ErrorMessages.EMAIL_INVALID);
    }

    /// <summary>
    /// Aplica regras de validação para o campo Telefone
    /// </summary>
    protected void ApplyTelefoneRules(IRuleBuilder<T, string> ruleBuilder)
    {
        ruleBuilder
            .NotEmpty().WithMessage(ErrorMessages.PHONE_REQUIRED)
            .MaximumLength(15).WithMessage(ErrorMessages.PHONE_MAX_LENGTH);
    }

    /// <summary>
    /// Aplica regras de validação para o campo Senha
    /// </summary>
    protected void ApplySenhaRules(IRuleBuilder<T, string> ruleBuilder)
    {
        ruleBuilder
            .NotEmpty().WithMessage(ErrorMessages.PASSWORD_REQUIRED)
            .MinimumLength(6).WithMessage(ErrorMessages.PASSWORD_MIN_LENGTH);
    }

    /// <summary>
    /// Aplica regras de validação para o campo CPF
    /// </summary>
    protected void ApplyCpfRules(IRuleBuilder<T, string> ruleBuilder)
    {
        ruleBuilder
            .NotEmpty().WithMessage(ErrorMessages.CPF_REQUIRED)
            .Length(11).WithMessage(ErrorMessages.CPF_LENGTH);
    }

    /// <summary>
    /// Aplica regras de validação para o campo Data de Nascimento
    /// </summary>
    protected void ApplyDataDeNascimentoRules(IRuleBuilder<T, DateTime> ruleBuilder)
    {
        ruleBuilder
            .NotEmpty().WithMessage(ErrorMessages.BIRTHDATE_REQUIRED)
            .LessThan(DateTime.Now).WithMessage(ErrorMessages.BIRTHDATE_INVALID);
    }
}
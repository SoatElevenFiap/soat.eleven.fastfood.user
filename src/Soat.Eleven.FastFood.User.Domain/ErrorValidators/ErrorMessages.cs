namespace Soat.Eleven.FastFood.User.Domain.ErrorValidators;

public static class ErrorMessages
{
    // Mensagens de ID
    public const string ID_REQUIRED = "O ID é obrigatório.";
    
    // Mensagens de Nome
    public const string NAME_REQUIRED = "O nome é obrigatório.";
    public const string NAME_MAX_LENGTH = "O nome deve ter no máximo 100 caracteres.";
    
    // Mensagens de Email
    public const string EMAIL_REQUIRED = "O email é obrigatório.";
    public const string EMAIL_INVALID = "O email deve ser um endereço válido.";
    
    // Mensagens de Telefone
    public const string PHONE_REQUIRED = "O telefone é obrigatório.";
    public const string PHONE_MAX_LENGTH = "O telefone deve ter no máximo 15 caracteres.";
    
    // Mensagens de Cliente ID
    public const string CLIENTE_ID_REQUIRED = "O ID do cliente é obrigatório.";
    
    // Mensagens de CPF
    public const string CPF_REQUIRED = "O CPF é obrigatório.";
    public const string CPF_LENGTH = "O CPF deve ter exatamente 11 caracteres.";
    
    // Mensagens de Data de Nascimento
    public const string BIRTHDATE_REQUIRED = "A data de nascimento é obrigatória.";
    public const string BIRTHDATE_INVALID = "A data de nascimento deve ser anterior à data atual.";
    
    // Mensagens de Senha
    public const string PASSWORD_REQUIRED = "A senha é obrigatória.";
    public const string PASSWORD_MIN_LENGTH = "A senha deve ter no mínimo 6 caracteres.";
    public const string CURRENT_PASSWORD_REQUIRED = "A senha atual é obrigatória.";
    public const string NEW_PASSWORD_REQUIRED = "A nova senha é obrigatória.";
    public const string NEW_PASSWORD_MIN_LENGTH = "A nova senha deve ter no mínimo 6 caracteres.";
    public const string NEW_PASSWORD_DIFFERENT = "A nova senha deve ser diferente da senha atual.";
}

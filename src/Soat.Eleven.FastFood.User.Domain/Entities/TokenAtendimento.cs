namespace Soat.Eleven.FastFood.User.Domain.Entities;

public class TokenAtendimento: IEntity
{
    public Guid Id { get; set; }
    public Guid? ClienteId { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime ModificadoEm { get; set; }
    public string? Cpf { get; set; }
    public Cliente? Cliente { get; set; }
}

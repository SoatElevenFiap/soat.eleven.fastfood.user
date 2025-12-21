namespace Soat.Eleven.FastFood.User.Domain.Entities;

public class Cliente : IEntity
{
    public Guid Id { get; set; }
    public string Cpf { get; set; }
    public DateTime DataDeNascimento { get; set; }
    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime ModificadoEm { get; set; }
}

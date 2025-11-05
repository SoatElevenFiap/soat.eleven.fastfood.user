namespace Soat.Eleven.FastFood.User.Domain.Entities;

public class Cliente : Usuario
{
    public string Cpf { get; set; }
    public DateTime DataDeNascimento { get; set; }
    public Guid ClienteId { get; set; }
}

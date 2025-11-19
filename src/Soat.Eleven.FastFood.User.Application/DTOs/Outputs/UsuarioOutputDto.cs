namespace Soat.Eleven.FastFood.User.Application.DTOs.Outputs;

public class UsuarioOutputDto
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Cpf { get; set; }
    public DateTime DataDeNascimento { get; set; }
}

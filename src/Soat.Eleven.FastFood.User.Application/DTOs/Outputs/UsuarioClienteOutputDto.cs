using Soat.Eleven.FastFood.User.Domain.Entities;

namespace Soat.Eleven.FastFood.User.Application.DTOs.Outputs;

public class UsuarioClienteOutputDto
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Cpf { get; set; }
    public DateTime DataDeNascimento { get; set; }

    public static implicit operator UsuarioClienteOutputDto(Cliente cliente) =>
        new UsuarioClienteOutputDto
        {
            Id = cliente.Usuario.Id,
            ClientId = cliente.Id,
            Nome = cliente.Usuario.Nome,
            Email = cliente.Usuario.Email,
            Telefone = cliente.Usuario.Telefone,
            Cpf = cliente.Cpf,
            DataDeNascimento = cliente.DataDeNascimento
        };

    public static explicit operator UsuarioClienteOutputDto(Usuario usuario)
    {
        return new UsuarioClienteOutputDto
        {
            Id = usuario.Id,
            ClientId = usuario.Cliente?.Id ?? Guid.Empty,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Telefone = usuario.Telefone,
            Cpf = usuario.Cliente?.Cpf ?? string.Empty,
            DataDeNascimento = usuario.Cliente?.DataDeNascimento ?? DateTime.MinValue
        };
    }
}

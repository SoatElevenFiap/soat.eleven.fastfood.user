using Soat.Eleven.FastFood.User.Domain.Entities;

namespace Soat.Eleven.FastFood.User.Application.DTOs.Outputs;

public class UsuarioAdmOutputDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }

    public static explicit operator UsuarioAdmOutputDto(Usuario usuario)
    {
        return new UsuarioAdmOutputDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Telefone = usuario.Telefone
        };
    }
}

using Soat.Eleven.FastFood.User.Domain.Entities;
using Soat.Eleven.FastFood.User.Domain.Enums;

namespace Soat.Eleven.FastFood.User.Application.DTOs.Inputs;

public class CriarAdmInputDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Telefone { get; set; }

    public static implicit operator Usuario(CriarAdmInputDto dto) =>
        new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Senha = dto.Senha,
            Telefone = dto.Telefone,
            Perfil = PerfilUsuario.Administrador
        };
}

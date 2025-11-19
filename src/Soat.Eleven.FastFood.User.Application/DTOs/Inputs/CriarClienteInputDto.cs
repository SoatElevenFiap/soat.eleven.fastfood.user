using Soat.Eleven.FastFood.User.Domain.Entities;

namespace Soat.Eleven.FastFood.User.Application.DTOs.Inputs;

public class CriarClienteInputDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Telefone { get; set; }
    public string Cpf { get; set; }
    public DateTime DataDeNascimento { get; set; }

    public static implicit operator Cliente(CriarClienteInputDto dto) =>
        new Cliente
        {
            Usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha,
                Telefone = dto.Telefone,
                Perfil = Domain.Enums.PerfilUsuario.Cliente
            },
            Cpf = dto.Cpf,
            DataDeNascimento = dto.DataDeNascimento
        };
}

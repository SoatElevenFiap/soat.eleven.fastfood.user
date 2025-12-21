using Soat.Eleven.FastFood.User.Domain.Entities;

namespace Soat.Eleven.FastFood.User.Application.DTOs.Inputs;

public class AtualizaClienteInputDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public Guid ClienteId { get; set; }
    public string Cpf { get; set; }
    public DateTime DataDeNascimento { get; set; }

    public static implicit operator Cliente(AtualizaClienteInputDto dto) =>
        new Cliente
        {
            Id = dto.ClienteId,
            Usuario = new Usuario
            {
                Id = dto.Id,
                Nome = dto.Nome,
                Email = dto.Email,
                Telefone = dto.Telefone
            },
            Cpf = dto.Cpf,
            DataDeNascimento = dto.DataDeNascimento
        };
}

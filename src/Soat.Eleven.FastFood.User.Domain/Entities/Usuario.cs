using Soat.Eleven.FastFood.User.Domain.Enums;

namespace Soat.Eleven.FastFood.User.Domain.Entities;

public class Usuario : IEntity
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Telefone { get; set; }
    public PerfilUsuario Perfil { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime ModificadoEm { get; set; }
    public StatusUsuario Status { get; set; }
    public Cliente? Cliente { get; set; }

    public void CriarCliente(DateTime dataDeNascimento, string cpf)
    {
        Cliente = new Cliente
        {
            DataDeNascimento = dataDeNascimento,
            Cpf = cpf
        };
    }
}

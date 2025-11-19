using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.DTOs.Outputs;
using Soat.Eleven.FastFood.User.Application.Handlers;

namespace Soat.Eleven.FastFood.User.Application.Interfaces.Handlers;

public interface IClienteHandler
{
    Task<Response> InserirCliente(CriarClienteInputDto input);
    Task<Response> AtualizarCliente(AtualizaClienteInputDto input);
    Task<Response> GetClienteByCPF(string cpf);
}

using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.DTOs.Outputs;
using Soat.Eleven.FastFood.User.Application.Handlers;

namespace Soat.Eleven.FastFood.User.Application.Interfaces.Handlers;

public interface IClienteHandler
{
    Task<ResponseHandler> InserirCliente(CriarClienteInputDto input);
    Task<ResponseHandler> AtualizarCliente(AtualizaClienteInputDto input);
    Task<ResponseHandler> GetClienteByCPF(string cpf);
}

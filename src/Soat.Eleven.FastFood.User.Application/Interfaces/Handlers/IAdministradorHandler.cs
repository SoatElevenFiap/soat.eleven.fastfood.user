using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.DTOs.Outputs;
using Soat.Eleven.FastFood.User.Application.Handlers;

namespace Soat.Eleven.FastFood.User.Application.Interfaces.Handlers;

public interface IAdministradorHandler
{
    Task<ResponseHandler> CriarAdministrador(CriarAdmInputDto input);
    Task<ResponseHandler> AtualizarAdminstrador(AtualizaAdmInputDto input);
}

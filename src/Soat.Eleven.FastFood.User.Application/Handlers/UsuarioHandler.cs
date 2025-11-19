using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.DTOs.Outputs;
using Soat.Eleven.FastFood.User.Application.Interfaces.Handlers;
using Soat.Eleven.FastFood.User.Application.Validators;
using Soat.Eleven.FastFood.User.Domain.Interfaces;

namespace Soat.Eleven.FastFood.User.Application.Handlers;

public class UsuarioHandler : BaseHandler, IUsuarioHandler
{
    private readonly IUsuarioRepository usuarioRepository;

    public UsuarioHandler(IUsuarioRepository usuarioRepository)
    {
        this.usuarioRepository = usuarioRepository;
    }

    public async Task<Response> AtualizarSenha(AtualizarSenhaInputDto input)
    {
        if (Validate(new AtualizarSenhaValidator(), input))
            return SendError();


    }

    public async Task<Response> GetUsuario()
    {
        
    }
}

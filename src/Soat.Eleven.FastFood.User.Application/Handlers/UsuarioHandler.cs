using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.DTOs.Outputs;
using Soat.Eleven.FastFood.User.Application.Interfaces.Handlers;
using Soat.Eleven.FastFood.User.Application.Validators;
using Soat.Eleven.FastFood.User.Domain.Interfaces.Repositories;
using Soat.Eleven.FastFood.User.Domain.Interfaces.Services;

namespace Soat.Eleven.FastFood.User.Application.Handlers;

public class UsuarioHandler : BaseHandler, IUsuarioHandler
{
    private readonly IUsuarioRepository usuarioRepository;
    private readonly IAuthenticationService authenticationService;
    private readonly IPasswordService passwordService;

    public UsuarioHandler(IUsuarioRepository usuarioRepository,
                          IAuthenticationService authenticationService,
                          IPasswordService passwordService)
    {
        this.usuarioRepository = usuarioRepository;
        this.authenticationService = authenticationService;
        this.passwordService = passwordService;
    }

    public async Task<ResponseHandler> AtualizarSenha(AtualizarSenhaInputDto input)
    {
        if (Validate(new AtualizarSenhaValidator(), input))
            return SendError();

        var usuario = authenticationService.GetUsuario();

        if (usuario is null)
            return SendError("Usuário não autenticado.");

        var currentPasswordHash = passwordService.Hash(input.CurrentPassword);

        if (currentPasswordHash != usuario.Senha)
            return SendError("Senha atual incorreta.");

        usuario.Senha = passwordService.Hash(input.NewPassword);

        usuarioRepository.Update(usuario);

        return SendSuccess(true);
    }

    public async Task<ResponseHandler> GetUsuario()
    {
        var usuario = authenticationService.GetUsuario();

        return SendSuccess((UsuarioOutputDto)usuario);
    }
}

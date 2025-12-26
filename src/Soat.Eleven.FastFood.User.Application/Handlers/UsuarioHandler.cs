using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.DTOs.Outputs;
using Soat.Eleven.FastFood.User.Application.Interfaces.Handlers;
using Soat.Eleven.FastFood.User.Application.Validators;
using Soat.Eleven.FastFood.User.Domain.ErrorValidators;
using Soat.Eleven.FastFood.User.Domain.Interfaces.Repositories;
using Soat.Eleven.FastFood.User.Domain.Interfaces.Services;

namespace Soat.Eleven.FastFood.User.Application.Handlers;

public class UsuarioHandler : BaseHandler, IUsuarioHandler
{
    private readonly IUsuarioRepository usuarioRepository;
    private readonly IJwtTokenService authenticationService;
    private readonly IPasswordService passwordService;

    public UsuarioHandler(IUsuarioRepository usuarioRepository,
                          IJwtTokenService authenticationService,
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

        var usuarioId = authenticationService.GetUsuarioId();
        var usuario = await usuarioRepository.GetByIdAsync(usuarioId);

        if (usuario is null)
            return SendError(ErrorMessages.UNAUTHENTICATED);

        var currentPasswordHash = passwordService.TransformToHash(input.CurrentPassword);

        if (currentPasswordHash != usuario.Senha)
            return SendError(ErrorMessages.INCORRECT_CURRENT_PASSWORD);

        usuario.Senha = passwordService.TransformToHash(input.NewPassword);

        usuarioRepository.Update(usuario);

        return SendSuccess(true);
    }

    public async Task<ResponseHandler> GetUsuario()
    {
        var usuarioId = authenticationService.GetUsuarioId();
        var usuario = await usuarioRepository.GetByIdAsync(usuarioId);

        if (usuario is null)
            return SendError(ErrorMessages.UNAUTHENTICATED);

        return usuario.Cliente is null ? SendSuccess((UsuarioOutputDto)usuario) : SendSuccess((UsuarioClienteOutputDto)usuario);
    }
}

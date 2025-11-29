using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.DTOs.Outputs;
using Soat.Eleven.FastFood.User.Application.Interfaces.Handlers;
using Soat.Eleven.FastFood.User.Application.Validators;
using Soat.Eleven.FastFood.User.Domain.Entities;
using Soat.Eleven.FastFood.User.Domain.Interfaces.Repositories;
using Soat.Eleven.FastFood.User.Domain.Interfaces.Services;

namespace Soat.Eleven.FastFood.User.Application.Handlers;

public class AdministradorHandler : BaseHandler, IAdministradorHandler
{
    private readonly IUsuarioRepository usuarioRepository;
    private readonly IAuthenticationService authenticationService;

    public AdministradorHandler(IUsuarioRepository usuarioRepository,
                                IAuthenticationService authenticationService)
    {
        this.usuarioRepository = usuarioRepository;
        this.authenticationService = authenticationService;
    }

    public async Task<ResponseHandler> AtualizarAdminstrador(AtualizaAdmInputDto input)
    {
        var administrador = authenticationService.GetUsuario();
        if (administrador is null)
            return SendError("Administrador não encontrado");

        var existeEmail = await usuarioRepository.ExistEmail(input.Email);

        if (existeEmail)
            AddError("Usuário já existe");

        if (Validate(new AtualizaAdmValidator(), input))
            return SendError();


        administrador.Nome = input.Nome;
        administrador.Email = input.Email;
        administrador.Telefone = input.Telefone;

        usuarioRepository.Update(administrador);

        return SendSuccess((UsuarioAdmOutputDto)administrador);
    }

    public async Task<ResponseHandler> CriarAdministrador(CriarAdmInputDto input)
    {
        var existeEmail = await usuarioRepository.ExistEmail(input.Email);

        if (existeEmail)
            return SendError("Usuário já existe");

        if (Validate(new CriarAdmValidator(), input))
            return SendError();

        var administrador = (Usuario)input;

        await usuarioRepository.AddAsync(administrador);

        return SendSuccess((UsuarioAdmOutputDto)administrador);
    }
}

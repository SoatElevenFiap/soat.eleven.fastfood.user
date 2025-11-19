using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.DTOs.Outputs;
using Soat.Eleven.FastFood.User.Application.Interfaces.Handlers;
using Soat.Eleven.FastFood.User.Application.Validators;
using Soat.Eleven.FastFood.User.Domain.Entities;
using Soat.Eleven.FastFood.User.Domain.Interfaces;

namespace Soat.Eleven.FastFood.User.Application.Handlers;

public class AdministradorHandler : BaseHandler, IAdministradorHandler
{
    private readonly IUsuarioRepository usuarioRepository;

    public AdministradorHandler(IUsuarioRepository usuarioRepository)
    {
        this.usuarioRepository = usuarioRepository;
    }

    public async Task<Response> AtualizarAdminstrador(AtualizaAdmInputDto input)
    {
        var administrador = await usuarioRepository.GetByIdAsync(input.Id);
        if (administrador is null)
        {
            AddError("Administrador não encontrado");
            return SendError();
        }

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

    public async Task<Response> CriarAdministrador(CriarAdmInputDto input)
    {
        var existeEmail = await usuarioRepository.ExistEmail(input.Email);

        if (existeEmail)
            AddError("Usuário já existe");

        if (Validate(new CriarAdmValidator(), input))
            return SendError();

        var administrador = (Usuario)input;

        await usuarioRepository.AddAsync(administrador);

        return SendSuccess((UsuarioAdmOutputDto)administrador);
    }
}

using FluentValidation;
using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.DTOs.Outputs;
using Soat.Eleven.FastFood.User.Application.Interfaces.Handlers;
using Soat.Eleven.FastFood.User.Application.Validators;
using Soat.Eleven.FastFood.User.Domain.Entities;
using Soat.Eleven.FastFood.User.Domain.Interfaces.Repositories;
using Soat.Eleven.FastFood.User.Domain.Interfaces.Services;

namespace Soat.Eleven.FastFood.User.Application.Handlers;

public class ClienteHandler : BaseHandler, IClienteHandler
{
    private readonly IClienteRepository clienteRepository;
    private readonly IUsuarioRepository usuarioRepository;
    private readonly IAuthenticationService authenticationService;

    public ClienteHandler(IClienteRepository clienteRepository,
                          IUsuarioRepository usuarioRepository,
                          IAuthenticationService authenticationService)
    {
        this.clienteRepository = clienteRepository;
        this.usuarioRepository = usuarioRepository;
        this.authenticationService = authenticationService;
    }

    public async Task<ResponseHandler> AtualizarCliente(AtualizaClienteInputDto input)
    {
        var usuarioId = authenticationService.GetUsuarioId();

        if (usuarioId == Guid.Empty)
            return SendError("Usuário não autenticado");

        var existeEmail = await usuarioRepository.ExistEmail(input.Email);

        if (existeEmail)
            return SendError("Usuário já existe");

        var existeCpf = await clienteRepository.ExistByCpf(input.Cpf);

        if (existeCpf)
            AddError("Usuário já existe");

        if (Validate(new AtualizaClienteValidator(), input))
            return SendError();

        var cliente = await clienteRepository.GetByIdAsync(usuarioId);

        cliente.Usuario.Nome = input.Nome;
        cliente.Usuario.Email = input.Email;
        cliente.Usuario.Telefone = input.Telefone;
        cliente.Cpf = input.Cpf;
        cliente.DataDeNascimento = input.DataDeNascimento;

        clienteRepository.Update(cliente);

        return SendSuccess((UsuarioClienteOutputDto)cliente);
    }

    public async Task<ResponseHandler> GetClienteByCPF(string cpf)
    {
        var usuario = await clienteRepository.GetByCPF(cpf);

        var result = usuario is null ? null : new UsuarioClienteOutputDto
        {
            Id = usuario.Id,
            ClientId = usuario.Id,
            Nome = usuario.Usuario.Nome,
            Email = usuario.Usuario.Email,
            Telefone = usuario.Usuario.Telefone,
            Cpf = usuario.Cpf,
            DataDeNascimento = usuario.DataDeNascimento
        };

        return SendSuccess(result);
    }

    public async Task<ResponseHandler> InserirCliente(CriarClienteInputDto input)
    {
        var existeEmail = await usuarioRepository.ExistEmail(input.Email);

        if (existeEmail)
            return SendError("Usuário já existe");

        var existeCpf = await clienteRepository.ExistByCpf(input.Cpf);

        if (existeCpf)
            return SendError("Usuário já existe");

        if (Validate(new CriarClienteValidator(), input))
            return SendError();

        var cliente = (Cliente)input;

        clienteRepository.Update(cliente);

        return SendSuccess((UsuarioClienteOutputDto)cliente);
    }
}

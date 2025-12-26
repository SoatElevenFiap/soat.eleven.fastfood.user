using FluentValidation;
using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.DTOs.Outputs;
using Soat.Eleven.FastFood.User.Application.Interfaces.Handlers;
using Soat.Eleven.FastFood.User.Application.Validators;
using Soat.Eleven.FastFood.User.Domain.Entities;
using Soat.Eleven.FastFood.User.Domain.ErrorValidators;
using Soat.Eleven.FastFood.User.Domain.Interfaces.Repositories;
using Soat.Eleven.FastFood.User.Domain.Interfaces.Services;

namespace Soat.Eleven.FastFood.User.Application.Handlers;

public class ClienteHandler : BaseHandler, IClienteHandler
{
    private readonly IClienteRepository clienteRepository;
    private readonly IUsuarioRepository usuarioRepository;
    private readonly IJwtTokenService authenticationService;
    private readonly IPasswordService passwordService;

    public ClienteHandler(IClienteRepository clienteRepository,
                          IUsuarioRepository usuarioRepository,
                          IJwtTokenService authenticationService,
                          IPasswordService passwordService)
    {
        this.clienteRepository = clienteRepository;
        this.usuarioRepository = usuarioRepository;
        this.authenticationService = authenticationService;
        this.passwordService = passwordService;
    }

    public async Task<ResponseHandler> AtualizarCliente(AtualizaClienteInputDto input)
    {
        var usuarioId = authenticationService.GetUsuarioId();

        if (usuarioId == Guid.Empty)
            return SendError(ErrorMessages.UNAUTHENTICATED);

        var existeEmail = await usuarioRepository.ExistEmail(input.Email);

        if (existeEmail)
            return SendError(ErrorMessages.USER_FOUND);

        var existeCpf = await clienteRepository.ExistByCpf(input.Cpf);

        if (existeCpf)
            AddError(ErrorMessages.USER_FOUND);

        if (Validate(new AtualizaClienteValidator(), input))
            return SendError();

        var cliente = await clienteRepository.GetByIdAsync(usuarioId);

        if (cliente is null)
            return SendError(ErrorMessages.CLIENT_NOT_FOUND);

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
            return SendError(ErrorMessages.USER_FOUND);

        var existeCpf = await clienteRepository.ExistByCpf(input.Cpf);

        if (existeCpf)
            return SendError(ErrorMessages.USER_FOUND);

        if (Validate(new CriarClienteValidator(), input))
            return SendError();

        var cliente = (Cliente)input;

        cliente.Usuario.Senha = passwordService.TransformToHash(input.Senha);

        await clienteRepository.AddAsync(cliente);

        return SendSuccess((UsuarioClienteOutputDto)cliente);
    }
}

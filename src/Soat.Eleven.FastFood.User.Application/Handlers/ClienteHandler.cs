using FluentValidation;
using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.DTOs.Outputs;
using Soat.Eleven.FastFood.User.Application.Interfaces.Handlers;
using Soat.Eleven.FastFood.User.Application.Validators;
using Soat.Eleven.FastFood.User.Domain.Entities;
using Soat.Eleven.FastFood.User.Domain.Interfaces.Repositories;

namespace Soat.Eleven.FastFood.User.Application.Handlers;

public class ClienteHandler : BaseHandler, IClienteHandler
{
    private readonly IValidator<CriarClienteInputDto> criarClienteValidator;
    private readonly IValidator<AtualizaClienteInputDto> atualizaClienteValidator;
    private readonly IClienteRepository clienteRepository;
    private readonly IUsuarioRepository usuarioRepository;

    public ClienteHandler(IValidator<CriarClienteInputDto> criarClienteValidator,
                          IValidator<AtualizaClienteInputDto> atualizaClienteValidator,
                          IClienteRepository clienteRepository,
                          IUsuarioRepository usuarioRepository)
    {
        this.criarClienteValidator = criarClienteValidator;
        this.atualizaClienteValidator = atualizaClienteValidator;
        this.clienteRepository = clienteRepository;
        this.usuarioRepository = usuarioRepository;
    }

    public async Task<Response> AtualizarCliente(AtualizaClienteInputDto input)
    {
        var existeEmail = await usuarioRepository.ExistEmail(input.Email);

        if (existeEmail)
            AddError("Usuário já existe");

        var existeCpf = await clienteRepository.ExistByCpf(input.Cpf);

        if (existeCpf)
            AddError("Usuário já existe");

        if (Validate(new AtualizaClienteValidator(), input))
            return SendError();

        var cliente = (Cliente)input;

        await clienteRepository.AddAsync(cliente);

        return SendSuccess((UsuarioClienteOutputDto)cliente);
    }

    public async Task<Response> GetClienteByCPF(string cpf)
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

    public async Task<Response> InserirCliente(CriarClienteInputDto input)
    {
        var existeEmail = await usuarioRepository.ExistEmail(input.Email);

        if (existeEmail)
            AddError("Usuário já existe");

        var existeCpf = await clienteRepository.ExistByCpf(input.Cpf);

        if (existeCpf)
            AddError("Usuário já existe");

        if (Validate(new CriarClienteValidator(), input))
            return SendError();

        var cliente = (Cliente)input;

        clienteRepository.Update(cliente);

        return SendSuccess((UsuarioClienteOutputDto)cliente);
    }
}

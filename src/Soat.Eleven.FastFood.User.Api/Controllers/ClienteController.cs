using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.Interfaces.Handlers;
using Soat.Eleven.FastFood.User.Domain.Enums;

namespace Soat.Eleven.FastFood.User.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : BaseController
{
    private readonly IClienteHandler _clienteHandler;

    public ClienteController(IClienteHandler clienteHandler)
    {
        _clienteHandler = clienteHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CriarCliente([FromBody] CriarClienteInputDto input)
    {
        var response = await _clienteHandler.InserirCliente(input);
        return SendResponse(response);
    }

    [HttpPut]
    [Authorize(PolicyRole.Cliente)]
    public async Task<IActionResult> AtualizarCliente([FromBody] AtualizaClienteInputDto input)
    {
        var response = await _clienteHandler.AtualizarCliente(input);
        return SendResponse(response);
    }

    [HttpGet("{cpf}")]
    [Authorize(PolicyRole.Cliente)]
    public async Task<IActionResult> GetClienteByCpf(string cpf)
    {
        var response = await _clienteHandler.GetClienteByCPF(cpf);
        return SendResponse(response);
    }
}

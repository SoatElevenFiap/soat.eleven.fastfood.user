using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.Interfaces.Handlers;
using Soat.Eleven.FastFood.User.Domain.Enums;

namespace Soat.Eleven.FastFood.User.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : BaseController
{
    private readonly IUsuarioHandler _usuarioHandler;

    public UsuarioController(IUsuarioHandler usuarioHandler)
    {
        _usuarioHandler = usuarioHandler;
    }

    [HttpGet]
    [Authorize(PolicyRole.Commom)]
    public async Task<IActionResult> GetUsuario()
    {
        var response = await _usuarioHandler.GetUsuario();
        return SendResponse(response);
    }

    [HttpPut("senha")]
    [Authorize]
    public async Task<IActionResult> AtualizarSenha([FromBody] AtualizarSenhaInputDto input)
    {
        var response = await _usuarioHandler.AtualizarSenha(input);
        return SendResponse(response);
    }
}
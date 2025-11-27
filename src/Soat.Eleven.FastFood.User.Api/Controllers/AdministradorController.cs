using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.Interfaces.Handlers;

namespace Soat.Eleven.FastFood.User.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdministradorController : BaseController
{
    private readonly IAdministradorHandler _administradorHandler;

    public AdministradorController(IAdministradorHandler administradorHandler)
    {
        _administradorHandler = administradorHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CriarAdministrador([FromBody] CriarAdmInputDto input)
    {
        var response = await _administradorHandler.CriarAdministrador(input);
        return SendResponse(response);
    }

    [HttpPut]
    public async Task<IActionResult> AtualizarAdministrador([FromBody] AtualizaAdmInputDto input)
    {
        var response = await _administradorHandler.AtualizarAdminstrador(input);
        return SendResponse(response);
    }
}
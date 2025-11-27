using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.User.Application.Handlers;

namespace Soat.Eleven.FastFood.User.Api.Controllers;

public abstract class BaseController : ControllerBase
{
    protected IActionResult SendResponse(ResponseHandler response)
    {
        if (response.Success)
            return Ok(response.Data);
        else
            return BadRequest(response.Data);
    }
}

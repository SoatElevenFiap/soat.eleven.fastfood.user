using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Soat.Eleven.FastFood.User.Domain.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;

namespace Soat.Eleven.FastFood.User.Infra.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtTokenService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUsuarioId()
    {
        var id = ReadToken(JwtRegisteredClaimNames.Sub);

        return Guid.Parse(id);
    }

    private string ReadToken(string typeClaim)
    {
        var token = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ElementAt(0) ?? throw new AuthenticationFailureException("Usuário não autenticado");

        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token.Replace("Bearer ", string.Empty)) ?? throw new AuthenticationFailureException("Usuário não autenticado");

        return (jsonToken as JwtSecurityToken)!.Claims.First(x => x.Type == typeClaim).Value;
    }
}

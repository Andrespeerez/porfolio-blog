using System.Security.Claims;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Auth;

public class CookieSessionManager : ISessionManager
{
    private readonly IHttpContextAccessor _accessor;
    
    public CookieSessionManager(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public Task SignInAsync(User user)
    {
        var claims = new []
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        return _accessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }

    public Task SignOutAsync()
    {
        return _accessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public async Task<int?> GetCurrentUserIdAsync()
    {
        var userPrincipal = _accessor.HttpContext?.User;

        if (userPrincipal?.Identity?.IsAuthenticated != true)
        {
            return null;
        }

        var idClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (idClaim == null)
        {
            return null;
        }

        bool parsed = int.TryParse(idClaim, out var id);

        return parsed ? id : null;
    }
}
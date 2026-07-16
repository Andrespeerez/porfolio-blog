using System.Security.Claims;

namespace Infrastructure.Auth;

public class UserContext : IUserContext
{
    public readonly IHttpContextAccessor _accessor;

    public int? UserId
    {
        get
        {
            return int.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;
        }
    }

    public string? Email
    {
        get
        {
            return User?.FindFirstValue(ClaimTypes.Email);
        }
    }

    public bool IsAuthenticated 
    {
        get
        {
            return User?.Identity?.IsAuthenticated ?? false;    
        } 
    }

    public bool IsAdmin
    {
        get
        {
            return User?.IsInRole("admin") ?? false;
        }
    }

    public UserContext(
        IHttpContextAccessor accessor
    )
    {
        _accessor = accessor;    
    }

    private ClaimsPrincipal? User => _accessor.HttpContext?.User; 
}
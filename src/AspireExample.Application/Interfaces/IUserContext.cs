using System.Security.Claims;

namespace AspireExample.Application.Interfaces;

public interface IUserContext
{
    ClaimsPrincipal User { get; set; }
    bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;
    string? UserId => User?.Identity?.Name;
}

public class UserContext(ClaimsPrincipal user) : IUserContext
{
    public ClaimsPrincipal User { get; set; } = user;

}
using System.Security.Claims;

namespace SharedKernel.Interfaces;

public interface IUserContext
{
    ClaimsPrincipal User { get; set; }
    bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;
    string? UserId => User?.Identity?.Name;
}

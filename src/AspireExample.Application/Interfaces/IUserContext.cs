using System.Security.Claims;

namespace AspireExample.Application.Interfaces;

public interface IUserContext
{
    ClaimsPrincipal User { get; set; }
    bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;
}
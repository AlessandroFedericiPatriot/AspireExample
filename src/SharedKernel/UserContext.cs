using System.Security.Claims;

namespace SharedKernel;

public class UserContext(ClaimsPrincipal user) : IUserContext
{
    public ClaimsPrincipal User { get; set; } = user;

}
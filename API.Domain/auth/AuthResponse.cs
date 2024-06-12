using API.Domain.user;
using System.Security.Claims;

namespace API.Domain.auth
{
    public class AuthResponse
    {
        public UserDomain User;
        public ClaimsPrincipal ClaimsPrincipal;

        public AuthResponse(UserDomain user, ClaimsPrincipal claimsPrincipal)
        {
            User = user;
            ClaimsPrincipal = claimsPrincipal;
        }
    }
}

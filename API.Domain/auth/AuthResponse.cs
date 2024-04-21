using API.Domain.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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

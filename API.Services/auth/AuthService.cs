using API.Domain.auth;
using API.Services.Auth.Interfaces;
using System.Security.Cryptography;
using System.Security.Claims;
using System.Text;
using API.Domain.tools;
using API.Domain.user;
using API.Services.user.interfaces;

namespace API.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public Response Authorization(string login, string password)
        {
            Response response = new Response();
            String passwordHash = GetPasswordHash(password);

            UserDomain? user = _userService.GetUserByLoginAndPasswordHash(login, passwordHash);

            if (user == null)
            {
                response.AddError("Неверный логин или пароль");
            }
            else
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                response.Data = new AuthResponse(user, claimsPrincipal);
            }
            

            return response;
        }
        
        public Response CheckAuthAndPermission(UserDomain systemUser)
        {
            Response response;

            if (systemUser?.Role == 1)
            {
                response = new Response("adminPanel/home");
            }
            else if (systemUser?.Role == 2)
            {
                response = new Response("practices");
            }
            else
            {
                response = new Response("login");
            }

            return response;
        }

        public static string GetPasswordHash(string password)
        {
            Byte[] bytes = Encoding.Unicode.GetBytes(password);
            MD5CryptoServiceProvider cryptoService = new MD5CryptoServiceProvider();
            Byte[] byteHash = cryptoService.ComputeHash(bytes);
            String hash = String.Empty;

            foreach (Byte b in byteHash)
                hash += String.Format("{0:x2}", b);

            return hash;
        }
    }
}

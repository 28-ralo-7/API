using API.Domain.auth;
using API.Domain.tools;
using API.Domain.user;
using API.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using static API.Tools.SessionExtensions;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<Response> Auth(string? login, string? password)
        {
            Response result = new Response();
            Response response = _authService.Authorization(login, password);

            if (response.IsSuccess)
            {
                string returnedUrl = "";
                AuthResponse authResponse = response.Data as AuthResponse;

                await HttpContext.SignInAsync(authResponse.ClaimsPrincipal);
                
                HttpContext.Session.Set<UserDomain>("user", authResponse.User);
                
                switch (authResponse.User.Role.Id)
                {
                    case (int)Role.PracticeLead:
                        returnedUrl = "practices";
                        break;
                    case (int)Role.Administrator:
                        returnedUrl = "adminPanel/practiceSchedule";
                        break;
                }

                result = new Response(returnedUrl);

                return result;
            }

            result.AddErrors(response.Errors);

            return result;
        }

        [HttpGet]
        public Response CheckAuthAndPermission()
        {
            return _authService.CheckAuthAndPermission(SystemUser);;
        }

        [HttpPost]
        public async Task LogOn()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
        }
    }
}

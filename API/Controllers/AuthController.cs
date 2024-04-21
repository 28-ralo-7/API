using System.Security.Claims;
using API.Domain.auth;
using API.Domain.tools;
using API.Domain.user;
using API.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action?]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public async Task<ActionResult> Auth(String login, String password)
        {
            Response response = _authService.Authorization(login, password);

            if (response.IsSuccess)
            {
                string returnedUrl = "";
                AuthResponse authResponse = response.Data as AuthResponse;

                await HttpContext.SignInAsync(authResponse.ClaimsPrincipal);

                HttpContext.Items["SystemUser"] = authResponse.User;
                    
                switch (authResponse.User.Role)
                {
                    case (int)Role.Administrator:
                        returnedUrl = "groups";
                        break;
                    case (int)Role.PracticeLead:
                        returnedUrl = "adminPanel/home";
                        break;
                }
                return Ok(returnedUrl);
            }

            return Unauthorized(response.Errors);
        }
    }
}

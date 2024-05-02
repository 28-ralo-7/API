﻿using API.Domain.auth;
using API.Domain.tools;
using API.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

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

                HttpContext.Items["SystemUser"] = authResponse.User;
                    
                switch (authResponse.User.Role)
                {
                    case (int)Role.PracticeLead:
                        returnedUrl = "groups";
                        break;
                    case (int)Role.Administrator:
                        returnedUrl = "adminPanel/home";
                        break;
                }

                result = new Response(returnedUrl);

                return result;
            }

            result.AddErrors(response.Errors);

            return result;
        }
    }
}

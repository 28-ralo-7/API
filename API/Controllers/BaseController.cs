using API.Domain.user;
using API.Tools;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
public class BaseController : Controller
{
    protected UserDomain SystemUser => HttpContext.Session.Get<UserDomain>("user");
}
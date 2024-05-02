using API.Domain.user;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
public class BaseController : Controller
{
    protected UserDomain SystemUser => HttpContext.Items["SystemUser"] as UserDomain;
}
using API.Domain.user;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BaseController : ControllerBase
{
    protected UserDomain SystemUser => HttpContext.Items["SystemUser"] as UserDomain;

}
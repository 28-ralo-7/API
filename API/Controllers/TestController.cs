using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TestController : BaseController
{
    [HttpGet]
    public ActionResult GetTest()
    {
        return Ok("asd");
    }
}
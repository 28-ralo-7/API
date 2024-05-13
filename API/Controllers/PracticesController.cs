using API.Domain.shared;
using API.Domain.tools;
using API.Services.practice.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PracticesController : BaseController
{
    private readonly IPracticeService _practiceService;

    public PracticesController(IPracticeService practiceService)
    {
        _practiceService = practiceService;
    }

    [Authorize(Roles = "2")]
    [HttpGet]
    public Response GetPracticeItemListByPermissions()
    {
        Response response = new Response();
        Item[] practiceItems = _practiceService.GetPracticeItemsByPermissions(SystemUser);
        
        if (practiceItems == null || practiceItems.Length == 0)
        {
            response.AddError("Практики не найдены");
        }
        else
        {
            response = new Response(practiceItems);
        }
        
        return response;
    }
}
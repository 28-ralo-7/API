using API.Domain.practice;
using API.Domain.practice.domain;
using API.Domain.practice.view;
using API.Domain.shared;
using API.Domain.tools;
using API.Services.practice.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PracticeController : BaseController
{
    private readonly IPracticeService _practiceService;

    public PracticeController(IPracticeService practiceService)
    {
        _practiceService = practiceService;
    } 

    [Authorize(Roles = "2")]
    [HttpGet]
    public Response GetPracticeLogByPracticeId(string practiceId)
    {
        Response response = new Response();
        PracticeLogDomain? practiceLogDomain = _practiceService.GetPracticeLogByPracticeId(practiceId);

        PracticeLogView practiceLogView = new PracticeLogView(practiceLogDomain);
        
        if (practiceLogView.Id == null)
        {
            response.AddError("Журнал практики не найден");
        }
        if (practiceLogView.LogItems?.Length == 0)
        {
            response.AddError("В данной группе нет студентов");
        }

        if (response.Errors.Count == 0)
        {
            response = new Response(practiceLogView);
        }
        
        return response;
    }
}
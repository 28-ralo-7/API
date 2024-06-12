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
    private readonly IPracticeLogService _practiceLogService;

    public PracticeController(IPracticeService practiceService, IPracticeLogService practiceLogService)
    {
        _practiceService = practiceService;
        _practiceLogService = practiceLogService;
    } 

    [Authorize(Roles = "2")]
    [HttpGet]
    public Response GetPracticeLogByPracticeId(string practiceId)
    {
        Response response = new Response();
        PracticeLogDomain? practiceLogDomain = _practiceLogService.GetPracticeLogByPracticeId(practiceId);

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
    
    [Authorize(Roles = "2")]
    [HttpGet]
    public Response GetCompanies()
    {
        Response response = new Response();
        Item[] companies = _practiceService.GetCompanies();

        if (companies.Length == 0)
        {
            response.AddError("Компании отсутствуют");
        }
        else
        {
            response = new Response(companies);
        }

        return response;
    }
    
    [Authorize(Roles = "2")]
    [HttpPost]
    public Response SavePracticeLogGrade(string logId, string? grade)
    {
        _practiceService.SavePracticeLogGrade(logId, grade);

        return new Response();
    }
    
    [Authorize(Roles = "2")]
    [HttpPost]
    public Response SavePracticeLogCompany(string logId, string companyName)
    {
        _practiceService.SavePracticeLogCompany(logId, companyName);

        return new Response();
    }

    [Authorize(Roles = "2")]
    [HttpPost]
    public Response UploadContract(string logId)
    {
        IFormFile file = Request.Form.Files[0];
        Response response = _practiceService.UploadContract(file, logId);

        return response;
    }
    
    [Authorize(Roles = "2")]
    [HttpPost]
    public Response UploadReport(string logId)
    {
        IFormFile file = Request.Form.Files[0];
        Response response = _practiceService.UploadReport(file, logId); 

        return response;
    }

}
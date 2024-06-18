using API.Domain.practice.domain;
using API.Domain.practice.view;
using API.Domain.shared;
using API.Domain.tools;
using API.Services.practice.interfaces;
using API.Services.tools.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PracticeController : BaseController
{
	private readonly IPracticeService _practiceService;
	private readonly IPracticeLogService _practiceLogService;
	private readonly IFileService _fileService;

	public PracticeController(IPracticeService practiceService, IPracticeLogService practiceLogService, IFileService fileService)
	{
		_practiceService = practiceService;
		_practiceLogService = practiceLogService;
		_fileService = fileService;
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
		Response response = _fileService.UploadContract(file, logId);

		return response;
	}
	
	[Authorize(Roles = "2")]
	[HttpPost]
	public Response UploadReport(string logId)
	{
		IFormFile file = Request.Form.Files[0];
		Response response = _fileService.UploadReport(file, logId); 

		return response;
	}

	[Authorize(Roles = "2")]
	[HttpPost]
	public Response RemoveContract(string logId)
	{
		Response response = _fileService.RemoveContract(logId);

		return response;
	}
	
	[Authorize(Roles = "2")]
	[HttpPost]
	public Response RemoveReport(string logId)
	{
		Response response = _fileService.RemoveReport(logId); 

		return response;
	}

	[Authorize(Roles = "2")]
	[HttpPost]
	public FileResult DownloadContract(string logId)
	{
		FileResponse response = _fileService.DownloadContract(logId);
		Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

		return File(response.Content, response.Extension, response.FileName);
	}

	[Authorize(Roles = "2")]
	[HttpPost]
	public FileResult DownloadReport(string logId)
	{
		FileResponse response = _fileService.DownloadReport(logId);
		Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

		return File(response.Content, response.Extension, response.FileName);
	}
}
using API.Database;
using API.Domain.tools;
using API.Domain.user;
using API.Services.practice.interfaces;
using API.Services.tools.interfaces;
using API.Services.user.interfaces;
using Microsoft.AspNetCore.Http;

namespace API.Services.tools;

public class FileService : IFileService
{
	private readonly IPracticeRepository _practiceRepository;
	private readonly IUserService _userService;

	public FileService(IPracticeRepository practiceRepository, IUserService userService)
	{
		_practiceRepository = practiceRepository;
		_userService = userService;
	}

	public FileResponse DownloadContract(string logId)
	{
		Guid logGuid = Guid.Parse(logId);

		Practicelog? log = _practiceRepository.GetPracticeLogsById(logGuid);
		UserDomain userDomain = _userService.GetUserById(log.Userid);

		FileInfo fileInfo = new FileInfo(log.Contract);

		byte[] buffer = System.IO.File.ReadAllBytes(fileInfo.FullName);

		string fileExtension = "application" + fileInfo.Extension.Replace('.', '/');
		string fileName = userDomain.Surname + "_contract" + fileInfo.Extension; 

		return new FileResponse(buffer, fileExtension, fileName);
	}

	public FileResponse DownloadReport(string logId)
	{
		Guid logGuid = Guid.Parse(logId);

		Practicelog? log = _practiceRepository.GetPracticeLogsById(logGuid);
		UserDomain userDomain = _userService.GetUserById(log.Userid);

		FileInfo fileInfo = new FileInfo(log.Report);

		byte[] buffer = System.IO.File.ReadAllBytes(fileInfo.FullName);

		string fileExtension = "application" + fileInfo.Extension.Replace('.', '/');
		string fileName = userDomain.Surname + "_report" + fileInfo.Extension; 

		return new FileResponse(buffer, fileExtension, fileName);
	}

	public Response UploadContract(IFormFile file, string logId)
	{
		Response response = new Response();
		Guid logGuid = Guid.Parse(logId);
		Practicelog? log = _practiceRepository.GetPracticeLogsById(logGuid);

		string extension = Path.GetExtension(file.FileName);
		string path = $"./Storage/contract_{Guid.NewGuid()}{extension}";
		string[] validExtension = new[] { ".pdf", ".txt", ".doc", ".docx"};
		
		if (validExtension.Contains(extension) && log != null)
		{
			if (log.Contract != null)
			{
				File.Delete(log.Contract);
			}

			using (FileStream fileStream = new FileStream(path, FileMode.Create)) {
				file.CopyToAsync(fileStream);
			}

			log.Contract = path;
			_practiceRepository.EditPracticeLog(log);
		}
		else
		{
			response.AddError($"Некорректный тип файла. Допустимые форматы {String.Join(",", validExtension)}");
		}

		return response;
	}

	public Response UploadReport(IFormFile file, string logId)
	{
		Response response = new Response();
		Guid logGuid = Guid.Parse(logId);
		Practicelog? log = _practiceRepository.GetPracticeLogsById(logGuid);

		string extension = Path.GetExtension(file.FileName);
		string path = $"./Storage/report_{Guid.NewGuid()}{extension}";
		string[] validExtension = new[] { ".pdf", ".txt", ".doc", ".docx"};
		
		if (validExtension.Contains(extension) && log != null)
		{
			if (log.Report != null)
			{
				File.Delete(log.Report);
			}

			using (FileStream fileStream = new FileStream(path, FileMode.Create)) {
				file.CopyToAsync(fileStream);
			}

			log.Report = path;
			_practiceRepository.EditPracticeLog(log);
		}
		else
		{
			response.AddError($"Некорректный тип файла. Допустимые форматы {String.Join(",", validExtension)}");
		}

		return response;
	}
}
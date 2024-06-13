using API.Domain.tools;
using Microsoft.AspNetCore.Http;

namespace API.Services.tools.interfaces;

public interface IFileService
{
	FileResponse DownloadContract(string logId);
	FileResponse DownloadReport(string logId);
	Response UploadContract(IFormFile file, string logId);
	Response UploadReport(IFormFile file, string logId);
}
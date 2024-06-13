namespace API.Domain.tools;

public class FileResponse
{
	public string FileName { get; set; }
	public byte[] Content { get; set; }
	public string Extension { get; set; }

	public FileResponse(byte[] content, string extension, string fileName = "")
	{
		Content = content;
		Extension = extension;
		FileName = fileName;
	}
}
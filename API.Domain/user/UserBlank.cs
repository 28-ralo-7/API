namespace API.Domain.user;

public class UserBlank
{
	public string? Id { get;  set; }
	public string Surname { get;  set; }
	public string Name { get;  set; }
	public string? Patronymic { get;  set; }
	public string Login { get;  set; }
	public string? Password { get;  set; }
	public string RoleId { get;  set; }
	public string? GroupId { get;  set; }
}
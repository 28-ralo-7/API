using API.Domain.shared;

namespace API.Domain.user;

public class UserSettingView
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string Login { get; set; }
    public Item Role { get; set; }
    public Item? Group { get; set; }

    public UserSettingView(UserDomain domain)
    {
        Id = domain.Id.ToString();
        FullName = domain?.Surname + " " + domain?.Name + " " + domain?.Patronomic;
        if (domain != null)
        {
            Login = domain.Login;
            Role = new Item(domain.Role.Id.ToString(), domain.Role.Type);
            Group = domain.Group != null ? new Item(domain.Group.Id.ToString(), domain.Group.Name) : null;
        }
    }
}
using API.Domain.group;

namespace API.Domain.user;

public class UserSettingDomain
{
    public Guid Id { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronomic { get; set; }

    public string Login { get; set; } = null!;

    public Role Role { get; set; }

    public GroupDomain? Group { get; set; }

    public UserSettingDomain() {}
    
}
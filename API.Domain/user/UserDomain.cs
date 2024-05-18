using API.Domain.group;

namespace API.Domain.user;

public class UserDomain
{
    public Guid Id { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronomic { get; set; }

    public string Login { get; set; } = null!;

    public string Passwordhash { get; set; } = null!;

    public Database.Role Role { get; set; }

    public GroupDomain? Group { get; set; }

    public bool IsRemoved { get; set; }

    public UserDomain() {}
    
    public UserDomain(Guid id, string surname, string name, string? patronomic, string login, string passwordhash,  Database.Role role, GroupDomain? group, bool isRemoved)
    {
        Id = id;
        Surname = surname;
        Name = name;
        Patronomic = patronomic;
        Login = login;
        Passwordhash = passwordhash;
        Role = role;
        Group = group;
        IsRemoved = isRemoved;
    }

    public UserDomain(User user, GroupDomain groupDomain, Database.Role role)
    {
        Id = user.Id;
        Surname = user.Surname;
        Name = user.Name;
        Patronomic = user.Patronomic;
        Login = user.Login;
        Passwordhash = user.Passwordhash;
        Role = role;
        Group = groupDomain;
        IsRemoved = user.Isremoved;
    }
    
    
    
    
    
    
    
    
    
    
}
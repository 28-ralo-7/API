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

    public int Role { get; set; }

    public GroupDomain Group { get; set; }

    public bool IsRemoved { get; set; }

    public UserDomain(Guid id, string surname, string name, string? patronomic, string login, string passwordhash, int role, GroupDomain group, bool isRemoved)
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

    public UserDomain(User user, GroupDomain groupDomain)
    {
        Id = user.Id;
        Surname = user.Surname;
        Name = user.Name;
        Patronomic = user.Patronomic;
        Login = user.Login;
        Passwordhash = user.Passwordhash;
        Role = user.Roletype;
        Group = groupDomain;
        IsRemoved = user.Isremoved;
    }
    
    
    
    
    
    
    
    
    
    
}
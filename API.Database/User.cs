using System.ComponentModel.DataAnnotations;

namespace API;

public partial class User
{
    [Key]
    public Guid Id { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronomic { get; set; }

    public string Login { get; set; } = null!;

    public string Passwordhash { get; set; } = null!;

    public int Roletype { get; set; }

    public Guid? Groupid { get; set; }

    public bool Isremoved { get; set; }

    public User(Guid id, string surname, string name, string? patronomic, string login, string passwordhash, int roletype, Guid? groupid, bool isremoved)
    {
        Id = id;
        Surname = surname;
        Name = name;
        Patronomic = patronomic;
        Login = login;
        Passwordhash = passwordhash;
        Roletype = roletype;
        Groupid = groupid;
        Isremoved = isremoved;
    }
}

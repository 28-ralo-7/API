using System;
using System.Collections.Generic;

namespace API;

public partial class User
{
    public Guid Id { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronomic { get; set; }

    public string Login { get; set; } = null!;

    public string Passwordhash { get; set; } = null!;

    public int Roletype { get; set; }

    public Guid? Groupid { get; set; }

    public bool Isremoved { get; set; }
}

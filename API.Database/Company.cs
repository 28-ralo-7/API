﻿namespace API.Database;

public partial class Company
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Isremoved { get; set; }
}
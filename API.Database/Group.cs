namespace API.Database;

public partial class Group
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Isremoved { get; set; }
}

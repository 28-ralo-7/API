using System.ComponentModel.DataAnnotations;

namespace API.Database;

public partial class Group
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Isremoved { get; set; }

    public Group(Guid id, string name, bool isremoved = false)
    {
        Id = id;
        Name = name;
        Isremoved = isremoved;
    }
}

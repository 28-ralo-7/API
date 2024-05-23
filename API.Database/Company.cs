using System.ComponentModel.DataAnnotations;

namespace API.Database;

public partial class Company
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Isremoved { get; set; }

    public Company(Guid id, string name, bool isremoved = false)
    {
        Id = id;
        Name = name;
        Isremoved = isremoved;
    }
}

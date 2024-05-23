using System.ComponentModel.DataAnnotations;

namespace API.Database;

public partial class Practice
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Isremoved { get; set; }

    public Practice(Guid id, string name, bool isremoved = false)
    {
        Id = id;
        Name = name;
        Isremoved = isremoved;
    }
}

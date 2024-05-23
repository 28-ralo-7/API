using System.ComponentModel.DataAnnotations;

namespace API.Database;

public class Role
{
    [Key]
    public int Id { get; set; }

    public string Type { get; set; }

}
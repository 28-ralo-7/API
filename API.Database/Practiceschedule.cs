using System.ComponentModel.DataAnnotations;

namespace API.Database;

public partial class Practiceschedule
{
    [Key]
    public Guid Id { get; set; }

    public Guid Practiceid { get; set; }

    public Guid Groupid { get; set; }
    
    public Guid Practiceleadid { get; set; }

    public DateOnly Datestart { get; set; }

    public DateOnly Dateend { get; set; }

    public bool Isremoved { get; set; }
}

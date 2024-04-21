namespace API.Database;

public partial class Practiceschedule
{
    public Guid Id { get; set; }

    public Guid Practiceid { get; set; }

    public Guid Groupid { get; set; }

    public DateOnly Datestart { get; set; }

    public DateOnly Dateend { get; set; }

    public bool Isremoved { get; set; }
}

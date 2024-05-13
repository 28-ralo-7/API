namespace API.Database;

public partial class Practicelog
{
    public Guid Id { get; set; }

    public Guid Userid { get; set; }

    public Guid Practicescheduleid { get; set; }

    public int? Grade { get; set; }

    public string? Contract { get; set; }

    public string? Report { get; set; }

    public Guid? Companyid { get; set; }
    
    public Boolean Isremoved { get; set; }
}

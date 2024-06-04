using System.ComponentModel.DataAnnotations;

namespace API.Database;

public partial class Practicelog
{
    [Key]
    public Guid Id { get; set; }

    public Guid Userid { get; set; }

    public Guid Practicescheduleid { get; set; }

    public int? Grade { get; set; }

    public string? Contract { get; set; }

    public string? Report { get; set; }

    public Guid? Companyid { get; set; }
    
    public Boolean Isremoved { get; set; }

    public Practicelog(Guid id, Guid userid, Guid practicescheduleid, int? grade = null, string? contract = null, string? report = null, Guid? companyid = null, bool isremoved = false)
    {
        Id = id;
        Userid = userid;
        Practicescheduleid = practicescheduleid;
        Grade = grade;
        Contract = contract;
        Report = report;
        Companyid = companyid;
        Isremoved = isremoved;
    }
}

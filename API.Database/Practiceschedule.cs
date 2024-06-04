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

	public Practiceschedule(Guid id, Guid practiceid, Guid groupid, Guid practiceleadid, DateOnly datestart, DateOnly dateend, bool isremoved = false)
	{
		Id = id;
		Practiceid = practiceid;
		Groupid = groupid;
		Practiceleadid = practiceleadid;
		Datestart = datestart;
		Dateend = dateend;
		Isremoved = isremoved;
	}
}

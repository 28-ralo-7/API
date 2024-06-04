namespace API.Domain.practice.blank;

public class PracticeScheduleBlank
{
	public string? Id { get; set; }
	public string PracticeId { get; set; }
	public string GroupId { get; set; }
	public string PracticeLeadId { get; set; }
	public DateTime DateStart { get; set; }
	public DateTime DateEnd { get; set; }
}
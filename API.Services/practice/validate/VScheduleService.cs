using API.Database;
using API.Domain.practice.blank;
using API.Services.practice.interfaces;

namespace API.Services.practice.validate;

public class VScheduleService
{
	private readonly IPracticeRepository _practiceRepository;

	public VScheduleService(IPracticeRepository practiceRepository)
	{
		_practiceRepository = practiceRepository;
	}

	public List<string> ValidateScheduleBlank(PracticeScheduleBlank blank)
	{
		List<string> errors = new List<string>();

		DateOnly dateStart = DateOnly.FromDateTime(blank.DateStart);
		DateOnly dateEnd = DateOnly.FromDateTime(blank.DateEnd);

		Practiceschedule[] practiceschedules = _practiceRepository.GetAllPracticeSchedules();
		Guid? blankId = blank.Id != null ? Guid.Parse(blank.Id) : new Guid();
		Guid groupId = Guid.Parse(blank.GroupId);
		
		Practiceschedule? practiceschedule = practiceschedules.FirstOrDefault(ps => ps.Id == blankId);

		if (practiceschedule != null && practiceschedule.Groupid != groupId)
		{
			errors.Add("Нельзя менять группу для расписания практики");
		}
		
		if (dateStart > dateEnd)
		{
			errors.Add("Дата начала не может быть больше даты окончания");
		}

		Boolean isMoreThanOnePracticePerGroup = practiceschedules.FirstOrDefault(ps => ps.Id != blankId && ps.Groupid == Guid.Parse(blank.GroupId)
				&& (dateStart >= ps.Datestart && dateStart <= ps.Dateend
				|| dateEnd >= ps.Datestart && dateEnd <= ps.Dateend
				|| dateStart >= ps.Datestart && dateEnd <= ps.Dateend
				|| dateStart <= ps.Datestart && dateEnd >= ps.Dateend
		)) != null;

		if (isMoreThanOnePracticePerGroup)
		{
			errors.Add("У группы не может быть более одной практики одновременно");
		}

		return errors;
	}
}
using API.Database;
using API.Domain.practice.blank;
using API.Domain.practice.domain;
using API.Domain.shared;
using API.Domain.tools;
using API.Domain.user;
using API.Services.practice.interfaces;
using API.Services.practice.validate;

namespace API.Services.practice;

public class PracticeService : IPracticeService
{
	private readonly IPracticeRepository _practiceRepository;
	private readonly VScheduleService _vScheduleService;

	public PracticeService(IPracticeRepository practiceRepository, VScheduleService vScheduleService)
	{
		_practiceRepository = practiceRepository;
		_vScheduleService = vScheduleService;
	}

	public Item[] GetPracticeItemsByPermissions(UserDomain systemUser)
	{
		Practiceschedule[] practiceSchedules = _practiceRepository.GetPracticeSchedulesByUserId(systemUser.Id);
		Guid[] practiceIds = practiceSchedules
			.Select(ps => ps.Practiceid)
			.Distinct()
			.ToArray();

		Practice[] practices = _practiceRepository.GetPracticesByIds(practiceIds);

		Item[] items = practices
			.Select(practice => new Item(practice.Id.ToString(), practice.Name))
			.ToArray();

		return items;
	}

	public void RemovePracticeLogsByUserIds(Guid[] userIds)
	{
		Practicelog[] practicelogs = _practiceRepository.GetPracticeLogsByUserIds(userIds);

		for (int i = 0; i < practicelogs.Length; i++)
		{
			practicelogs[i].Isremoved = true;
		}
		
		_practiceRepository.RemovePracticeLogs(practicelogs);
	}

	public Item[] GetAllPractices()
	{
		Practice[] practices = _practiceRepository.GetAllPractices();
		Item[] options = practices
			.Select(practice => new Item(practice.Id.ToString(), practice.Name))
			.ToArray();

		return options;
	}

	public Response SavePractice(Item practice)
	{
		Response response = new Response();

		if (String.IsNullOrWhiteSpace(practice.Label))
		{
			response.AddError("Заполните название практики");
		}
		else
		{
			Guid id = practice.Value.Length > 0 ? Guid.Parse(practice.Value) : new Guid();

			Practice? existsPractice = _practiceRepository.GetAllPractices().FirstOrDefault(x => x.Name.Trim() == practice.Label.Trim());
			
			if (id != existsPractice?.Id && existsPractice?.Name.Trim() == practice.Label.Trim())
			{
				response.AddError("Такая практика уже есть");
			}
			else if (String.IsNullOrWhiteSpace(practice.Value))
			{
				AddPractice(practice);
			}
			else
			{
				EditPractice(practice);
			}
		}

		return response;
	}

	public Response RemovePractice(string practiceId)
	{
		Guid id = Guid.Parse(practiceId);
		Practice practice = _practiceRepository.GetPracticeById(id);

		Practiceschedule[] practiceschedules = _practiceRepository.GetPracticeSchedulesByPracticeId(practice.Id); 
		Guid[] psIds = practiceschedules.Select(ps => ps.Id).ToArray();

		Practicelog[] practicelogs = _practiceRepository.GetPracticeLogsByPracticescheduleIds(psIds);

		foreach (var ps in practiceschedules)
		{
			ps.Isremoved = true;
		}
		foreach (var pl in practicelogs)
		{
			pl.Isremoved = true;
		}
		
		practice.Isremoved = true;
		_practiceRepository.RemovePractice(practice);
		_practiceRepository.RemovePracticeSchedules(practiceschedules);
		_practiceRepository.RemovePracticeLogs(practicelogs);
		
		return new Response();
	}

	public void RemovePracticeSchedulesByGroupId(Guid groupId)
	{
		Practiceschedule[] practiceschedules = _practiceRepository.GetPracticeSchedulesByGroupId(groupId);

		for (int i = 0; i < practiceschedules.Length; i++)
		{
			practiceschedules[i].Isremoved = true;
		}
		
		_practiceRepository.RemovePracticeSchedules(practiceschedules);
	}

	public Response SaveSchedule(PracticeScheduleBlank blank)
	{
		Response response = new Response();
		List<string> errors = _vScheduleService.ValidateScheduleBlank(blank);

		if (errors.Count != 0)
		{
			response.AddErrors(errors);
		}
		else
		{
			if (blank.Id == null)
			{
				AddSchedule(blank);
			}
			else
			{
				EditSchedule(blank);
			}
		}

		return response;
	}

	public Response RemoveSchedule(string scheduleId)
	{
		Guid id = Guid.Parse(scheduleId);

		Practiceschedule practiceschedule = _practiceRepository.GetPracticeScheduleById(id);
		Practicelog[] practicelogs = _practiceRepository.GetPracticeLogsByPracticescheduleId(practiceschedule.Id);

		foreach (Practicelog log in practicelogs)
		{
			log.Isremoved = true;
		}

		practiceschedule.Isremoved = true;

		_practiceRepository.RemovePracticeSchedule(practiceschedule);
		_practiceRepository.RemovePracticeLogs(practicelogs);

		return new Response();
	}

	private void EditSchedule(PracticeScheduleBlank blank)
	{
		Guid id = Guid.Parse(blank.Id);
		Guid practiceid = Guid.Parse(blank.PracticeId);
		Guid groupid = Guid.Parse(blank.GroupId);
		Guid practiceleadid = Guid.Parse(blank.PracticeLeadId);
		DateOnly dateStart = DateOnly.FromDateTime(blank.DateStart);
		DateOnly dateEnd = DateOnly.FromDateTime(blank.DateEnd);

		Practiceschedule? practiceschedule = _practiceRepository.GetPracticeScheduleById(id);

		practiceschedule.Practiceid = practiceid;
		practiceschedule.Groupid = groupid;
		practiceschedule.Practiceleadid = practiceleadid;
		practiceschedule.Datestart = dateStart;
		practiceschedule.Dateend = dateEnd;

		_practiceRepository.EditPracticeSchedule(practiceschedule);
	}

	private void AddSchedule(PracticeScheduleBlank blank)
	{
		Guid id = Guid.NewGuid();
		Guid practiceid = Guid.Parse(blank.PracticeId);
		Guid groupid = Guid.Parse(blank.GroupId);
		Guid practiceleadid = Guid.Parse(blank.PracticeLeadId);
		DateOnly dateStart = DateOnly.FromDateTime(blank.DateStart);
		DateOnly dateEnd = DateOnly.FromDateTime(blank.DateEnd);

		Practiceschedule newSchedule = new Practiceschedule(id, practiceid, groupid, practiceleadid, dateStart, dateEnd);

		_practiceRepository.AddSchedule(newSchedule);
	}

	private void AddPractice(Item practice)
	{
		Guid id = Guid.NewGuid();
		Practice newPractice = new Practice(id, practice.Label);

		_practiceRepository.AddPractice(newPractice);
	}
	
	private void EditPractice(Item practice)
	{
		Guid id = Guid.Parse(practice.Value);
		Practice existsPractice = _practiceRepository.GetPracticeById(id);

		existsPractice.Name = practice.Label;

		_practiceRepository.EditPractice(existsPractice);
	}
}
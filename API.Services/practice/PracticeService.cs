using System.Reflection;
using System.Text;
using System.Text.Json;
using API.Database;
using API.Domain.group;
using API.Domain.practice.domain;
using API.Domain.shared;
using API.Domain.tools;
using API.Domain.user;
using API.Services.company.interfaces;
using API.Services.group.interfaces;
using API.Services.practice.interfaces;
using Microsoft.AspNetCore.Http;

namespace API.Services.practice;

public class PracticeService : IPracticeService
{
	private readonly IPracticeRepository _practiceRepository;
	private readonly ICompanyService _companyService;
	private readonly IGroupService _groupService;

	public PracticeService(IPracticeRepository practiceRepository, ICompanyService companyService, IGroupService groupService)
	{
		_practiceRepository = practiceRepository;
		_companyService = companyService;
		_groupService = groupService;
	}

	public Item[] GetPracticeItemsByPermissions(UserDomain systemUser)
	{
		Practiceschedule[] practiceSchedules = _practiceRepository.GetPracticeSchedulesByUserId(systemUser.Id);
		
		Guid[] practiceIds = practiceSchedules
			.Select(ps => ps.Practiceid)
			.Distinct()
			.ToArray();
		Guid[] groupIds = practiceSchedules.Select(ps => ps.Groupid).ToArray();

		GroupDomain[] groups = _groupService.GetGroupByIds(groupIds);
		Practice[] practices = _practiceRepository.GetPracticesByIds(practiceIds);

		List<Item> practiceOptions = new List<Item>();
		
		for (int i = 0; i < practiceSchedules.Length; i++)
		{
			GroupDomain group = groups.First(group => group.Id == practiceSchedules[i].Groupid);
			Practice practice = practices.First(practice => practice.Id == practiceSchedules[i].Practiceid);
			string practiceOptionName = practice.Name + $"({group.Name})";
			
			practiceOptions.Add(new Item(practiceSchedules[0].Id.ToString(), practiceOptionName));
		}

		return practiceOptions.ToArray();
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

	public Item[] GetCompanies()
	{
		return _companyService.GetAllCompany();

	}

	public void SavePracticeLogGrade(string logId, string grade)
	{
		bool id = Guid.TryParse(logId, out Guid guid);
		Practicelog? log = _practiceRepository.GetPracticeLogsById(guid);

		if (log != null)
		{
			int? newGrade = grade != null ? Convert.ToInt32(grade) : null;
			log.Grade = newGrade;

			_practiceRepository.EditPracticeLog(log);
		}
	}

	public void SavePracticeLogCompany(string logId, string companyName)
	{
		bool id = Guid.TryParse(logId, out Guid guid);
		string formatedCompanyName = companyName.Trim();
		Practicelog? log = _practiceRepository.GetPracticeLogsById(guid);

		if (log != null)
		{
			Item? existsCompany = _companyService.GetCompanyByName(formatedCompanyName);

			if (existsCompany == null)
			{
				Guid newCompanyId = Guid.NewGuid();
				Item newCompany = new Item(newCompanyId.ToString(), formatedCompanyName);

				log.Companyid = newCompanyId;

				_practiceRepository.EditPracticeLog(log);
				_companyService.SaveCompany(newCompany);
			}
			else
			{
				log.Companyid = Guid.Parse(existsCompany.Value);

				existsCompany.Label = formatedCompanyName;
				
				_practiceRepository.EditPracticeLog(log);
				_companyService.SaveCompany(existsCompany);
			}
		}
	}

	public Guid GetPracticeScheduleIdByGroupId(Guid? groupid)
	{
		Practiceschedule practiceschedule = _practiceRepository.GetPracticeScheduleByGroupId(groupid);

		return practiceschedule.Id ;

	}

	public void AddPracticeLogByUser(User user)
	{
		Practiceschedule[] practiceSchedules = _practiceRepository.GetPracticeSchedulesByGroupId(user.Groupid);

		for (int i = 0; i < practiceSchedules.Length; i++)
		{
			Practicelog log = new Practicelog(Guid.NewGuid(), user.Id, practiceSchedules[i].Id, null, null, null, null, false);
		
			_practiceRepository.AddPracticeLog(log);	
		}
	}

	public void ChangeUserLogByNewGroup(Guid? oldGroup, Guid newGroup, Guid userId)
	{
		Practiceschedule[] oldPracticeschedules = _practiceRepository.GetPracticeSchedulesByGroupId(oldGroup!);
		Practiceschedule[] newPracticeSchedules = _practiceRepository.GetPracticeSchedulesByGroupId(newGroup);

		if(oldPracticeschedules.Length != 0)	
		{
			_practiceRepository.RemovePracticeLogsByUserId(userId);
		}

		for (int i = 0; i < newPracticeSchedules.Length; i++)
		{
			Practicelog log = new Practicelog(Guid.NewGuid(), userId, newPracticeSchedules[i].Id, null, null, null, null,
				false);

			_practiceRepository.AddPracticeLog(log);
		}
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
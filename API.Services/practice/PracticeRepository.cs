using API.Database;
using API.Services.practice.interfaces;

namespace API.Services.practice;

public class PracticeRepository : IPracticeRepository
{
	private readonly PracticetrackerContext _context;

	public PracticeRepository(PracticetrackerContext context)
	{
		_context = context;
	}

	public Practiceschedule[] GetPracticeSchedulesByUserId(Guid userId)
	{
		return _context.Practiceschedules
			.Where(ps => (ps.Practiceleadid == userId) 
						 && ps.Isremoved != true)
			.ToArray();
	}

	public Practice[] GetPracticesByIds(Guid[] ids)
	{
		return _context.Practices
			.Where(practice => ids.Contains(practice.Id) 
							   && practice.Isremoved != true)
			.ToArray();
	}

	public Practice? GetPracticeById(Guid id)
	{
		return _context.Practices
			.FirstOrDefault(ps => ps.Id == id && ps.Isremoved != true && ps.Isremoved != true);
	}

	public Practiceschedule GetPracticeScheduleByPracticeId(Guid practiceId)
	{
		return _context.Practiceschedules
			.First(ps => ps.Practiceid == practiceId && !ps.Isremoved);
	}

	public Practiceschedule[] GetPracticeSchedulesByPracticeId(Guid practiceId)
	{
		return _context.Practiceschedules
			.Where(ps => ps.Practiceid == practiceId && !ps.Isremoved)
			.ToArray();
	}

	public Practicelog[] GetPracticeLogsByPracticescheduleId(Guid practiceScheduleId)
	{
		return _context.Practicelogs
			.Where(log => log.Practicescheduleid == practiceScheduleId && log.Isremoved != true)
			.ToArray();
	}

	public Practicelog[] GetPracticeLogsByPracticescheduleIds(Guid[] practiceScheduleIds)
	{
		return _context.Practicelogs
			.Where(log => practiceScheduleIds.Contains(log.Practicescheduleid)
						  && log.Isremoved != true)
			.ToArray();
	}

	public Practicelog[] GetPracticeLogsByUserIds(Guid[] userIds)
	{
		return _context.Practicelogs
			.Where(log => userIds.Contains(log.Userid))
			.ToArray();
	}

	public Practice[] GetAllPractices()
	{
		return _context.Practices
			.Where(practice => practice.Isremoved != true)
			.ToArray();
	}

	public void AddPractice(Practice newPractice)
	{
		_context.Practices.Add(newPractice);
		_context.SaveChanges();
	}

	public void EditPractice(Practice existsPractice)
	{
		_context.Practices.Update(existsPractice);
		_context.SaveChanges();
	}

	public void RemovePractice(Practice practice) //TODO: удалить файлы
	{
		_context.Practices.Update(practice);
		_context.SaveChanges();
	}

	public void RemovePracticeSchedules(Practiceschedule[] schedules)
	{
		_context.Practiceschedules.UpdateRange(schedules);
		_context.SaveChanges();
	}

	public void RemovePracticeLogs(Practicelog[] logs)
	{
		_context.Practicelogs.UpdateRange(logs);
		_context.SaveChanges();
	}

	public Practiceschedule[] GetPracticeSchedulesByGroupId(Guid? groupId)
	{
		return _context.Practiceschedules
			.Where(ps => ps.Groupid == groupId)
			.ToArray();
	}

	public Practiceschedule[] GetAllPracticeSchedules()
	{
		return _context.Practiceschedules
			.Where(ps => ps.Isremoved != true)
			.ToArray();
	}

	public void AddSchedule(Practiceschedule newSchedule)
	{
		_context.Practiceschedules.Add(newSchedule);
		_context.SaveChanges();
	}

	public void EditPracticeSchedule(Practiceschedule practiceschedule)
	{
		_context.Practiceschedules.Update(practiceschedule);
		_context.SaveChanges();
	}

	public void RemovePracticeSchedule(Practiceschedule schedule)
	{
		_context.Practiceschedules.Update(schedule);
		_context.SaveChanges();
	}

	public void AddPracticeLogs(Practicelog[] logs)
	{
		_context.Practicelogs.AddRange(logs);
		_context.SaveChanges();
	}

	public Practicelog? GetPracticeLogsById(Guid id)
	{
		return _context.Practicelogs.FirstOrDefault(log => log.Id == id);
	}

	public void EditPracticeLog(Practicelog log)
	{
		_context.Practicelogs.Update(log);
		_context.SaveChanges();
	}

	public Practiceschedule? GetPracticeScheduleByGroupId(Guid? groupid)
	{
		return _context.Practiceschedules.FirstOrDefault(ps => ps.Groupid == groupid);
	}

	public void AddPracticeLog(Practicelog log)
	{
		_context.Practicelogs.Add(log);
		_context.SaveChanges();
	}

	public Practicelog? GetPracticeLogsByUserId(Guid userId)
	{
		return _context.Practicelogs.FirstOrDefault(log => log.Userid == userId);
	}

	public void RemovePracticeLogsByUserId(Guid userId)
	{
		Practicelog log = _context.Practicelogs.First(log => log.Userid == userId);
		log.Isremoved = true;

		_context.Practicelogs.Update(log);
		_context.SaveChanges();
	}

	public Practiceschedule GetPracticeScheduleById(Guid id)
	{
		return _context.Practiceschedules
			.FirstOrDefault(ps => ps.Id == id && !ps.Isremoved);
	}
}
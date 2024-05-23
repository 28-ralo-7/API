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

    public Practice GetPracticeById(Guid id)
    {
        return _context.Practices
            .First(ps => ps.Id == id && ps.Isremoved != true && ps.Isremoved != true);
    }

    public Practiceschedule GetPracticeSchedulesByPracticeId(Guid practiceId)
    {
        return _context.Practiceschedules
            .First(ps => ps.Practiceid == practiceId && !ps.Isremoved);
    }

    public Practicelog[] GetPracticeLogsByPracticescheduleId(Guid practiceScheduleId)
    {
        return _context.Practicelogs
            .Where(log => log.Practicescheduleid == practiceScheduleId && log.Isremoved != true)
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
        _context.SaveChanges();;
    }

    public void EditPractice(Practice existsPractice)
    {
        _context.Practices.Update(existsPractice);
        _context.SaveChanges();
    }

    public void RemovePractice(string practiceId)
    {
        Guid id = Guid.Parse(practiceId);
        Practice practice = _context.Practices.First(practice => practice.Id == id);
        Practiceschedule[] practiceschedules = _context.Practiceschedules
            .Where(ps => ps.Practiceid == practice.Id)
            .ToArray();
        Guid[] psIds = practiceschedules.Select(ps => ps.Id).ToArray();

        Practicelog[] practicelogs = _context.Practicelogs
            .Where(pl => psIds.Contains(pl.Id))
            .ToArray();
        
        
        practice.Isremoved = true;
        _context.Practices.Update(practice);
        _context.SaveChanges();
    }
}
using API.Database;
using API.Services.practice.interfaces;
using Microsoft.EntityFrameworkCore;

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
                               && !practice.Isremoved)
            .ToArray();
    }

    public Practice GetPracticeById(Guid id)
    {
        return _context.Practices
            .FirstOrDefault(ps => ps.Id == id && ps.Isremoved != true && ps.Isremoved != true);
    }

    public Practiceschedule GetPracticeSchedulesByPracticeId(Guid practiceId)
    {
        return _context.Practiceschedules
            .FirstOrDefault(ps => ps.Practiceid == practiceId && !ps.Isremoved);
    }

    public Practicelog[] GetPracticeLogsByPracticescheduleId(Guid practiceScheduleId)
    {
        return _context.Practicelogs
            .Where(log => log.Practicescheduleid == practiceScheduleId && log.Isremoved != true)
            .ToArray();
    }
}
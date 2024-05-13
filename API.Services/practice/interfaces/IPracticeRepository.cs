using API.Database;

namespace API.Services.practice.interfaces;

public interface IPracticeRepository
{
    Practiceschedule[] GetPracticeSchedulesByUserId(Guid userId);
    Practice[] GetPracticesByIds(Guid[] ids);
    Practice GetPracticeById(Guid id);
    Practiceschedule GetPracticeSchedulesByPracticeId(Guid practiceId);
    Practicelog[] GetPracticeLogsByPracticescheduleId(Guid practiceScheduleId);
}
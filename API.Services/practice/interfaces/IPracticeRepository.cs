using API.Database;

namespace API.Services.practice.interfaces;

public interface IPracticeRepository
{
    Practiceschedule[] GetPracticeSchedulesByUserId(Guid userId);
    Practice[] GetPracticesByIds(Guid[] ids);
    Practice? GetPracticeById(Guid id);
    Practiceschedule GetPracticeScheduleByPracticeId(Guid practiceId);
    Practiceschedule[] GetPracticeSchedulesByPracticeId(Guid practiceId);
    Practicelog[] GetPracticeLogsByPracticescheduleId(Guid practiceScheduleId);
    Practicelog[] GetPracticeLogsByPracticescheduleIds(Guid[] practiceScheduleIds);
    Practicelog[] GetPracticeLogsByUserIds(Guid[] userIds);
    Practice[] GetAllPractices();
    void AddPractice(Practice newPractice);
    void EditPractice(Practice existsPractice);
    void RemovePractice(Practice practice);
    void RemovePracticeSchedules(Practiceschedule[] schedules);
    void RemovePracticeLogs(Practicelog[] logs);
    Practiceschedule[] GetPracticeSchedulesByGroupId(Guid groupId);
    Practiceschedule[] GetAllPracticeSchedules();
}
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
    void AddSchedule(Practiceschedule newSchedule);
    Practiceschedule GetPracticeScheduleById(Guid id);
    void EditPracticeSchedule(Practiceschedule practiceschedule);
    void RemovePracticeSchedule(Practiceschedule schedule);
    void AddPracticeLogs(Practicelog[] toArray);
    Practicelog? GetPracticeLogsById(Guid id);
    void EditPracticeLog(Practicelog log);
    void SaveContractPath(Practicelog log);
    void SaveReportPath(Practicelog log);
}
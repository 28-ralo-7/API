using API.Domain.practice;
using API.Domain.practice.blank;
using API.Domain.practice.domain;
using API.Domain.shared;
using API.Domain.tools;
using API.Domain.user;

namespace API.Services.practice.interfaces;

public interface IPracticeService
{
    Item[] GetPracticeItemsByPermissions(UserDomain systemUser);
    void RemovePracticeLogsByUserIds(Guid[] userIds);
    Item[] GetAllPractices();
    Response SavePractice(Item practice);
    Response RemovePractice(string practiceId);
    void RemovePracticeSchedulesByGroupId(Guid groupId);
    Response RemoveSchedule(string scheduleId);
    Item[] GetCompanies();
}
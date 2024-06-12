using API.Database;
using API.Domain.practice.domain;
using API.Domain.shared;
using API.Domain.tools;
using API.Domain.user;
using Microsoft.AspNetCore.Http;

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
    void SavePracticeLogGrade(string logId, string grade);
    void SavePracticeLogCompany(string logId, string companyName);
    Response UploadContract(IFormFile file, string logId);
    Response UploadReport(IFormFile file, string logId);
    Guid GetPracticeScheduleIdByGroupId(Guid? groupid);
    void AddPracticeLogByUser(User user);
    void ChangeUserLogByNewGroup(Guid? oldGroup, Guid newGroup, Guid userId);
    Response DownloadContract(string logId);
}
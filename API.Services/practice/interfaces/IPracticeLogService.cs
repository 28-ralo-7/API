using API.Database;
using API.Domain.practice.domain;

namespace API.Services.practice.interfaces;

public interface IPracticeLogService
{
    PracticeLogDomain GetPracticeLogByPracticeId(string practiceId);
    PracticeScheduleDomain[] GetPracticeSchedules();
}
using API.Domain.practice;
using API.Domain.practice.domain;
using API.Domain.shared;
using API.Domain.user;

namespace API.Services.practice.interfaces;

public interface IPracticeService
{
    Item[] GetPracticeItemsByPermissions(UserDomain systemUser);
    PracticeLogDomain GetPracticeLogByPracticeId(string practiceId);
}
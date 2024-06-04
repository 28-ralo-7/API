using API.Domain.practice.blank;
using API.Domain.tools;

namespace API.Services.practice.interfaces;

public interface ISavePracticeScheduleService
{
    Response SaveSchedule(PracticeScheduleBlank blank);
}
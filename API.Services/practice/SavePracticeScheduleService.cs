using API.Database;
using API.Domain.practice.blank;
using API.Domain.tools;
using API.Services.practice.interfaces;
using API.Services.practice.validate;
using API.Services.user.interfaces;

namespace API.Services.practice;

public class SavePracticeScheduleService : ISavePracticeScheduleService
{
    private readonly IUserService _userService;
    private readonly IPracticeRepository _practiceRepository;
    private readonly VScheduleService _vScheduleService;

    public SavePracticeScheduleService(VScheduleService vScheduleService, IPracticeRepository practiceRepository, IUserService userService)
    {
        _vScheduleService = vScheduleService;
        _practiceRepository = practiceRepository;
        _userService = userService;
    }

    public Response SaveSchedule(PracticeScheduleBlank blank)
    {
        Response response = new Response();
        List<string> errors = _vScheduleService.ValidateScheduleBlank(blank);

        if (errors.Count != 0)
        {
            response.AddErrors(errors);
        }
        else
        {
            if (blank.Id == null)
            {
                AddSchedule(blank);
            }
            else
            {
                EditSchedule(blank);
            }
        }

        return response;
    }
    
    public void CreatePracticeLogForSchedule(Guid groupId, Guid scheduleId)
    {
        User[] users = _userService.GetStudentsByGroupId(groupId);
        List<Practicelog> practicelogs = new List<Practicelog>();    
        
        foreach (User user in users)
        {
            Guid id = Guid.NewGuid();
            Practicelog practicelog = new Practicelog(id, user.Id, scheduleId);
            
            practicelogs.Add(practicelog);
        }
        
        _practiceRepository.AddPracticeLogs(practicelogs.ToArray());
    }
    
    
    private void EditSchedule(PracticeScheduleBlank blank)
    {
        Guid id = Guid.Parse(blank.Id);
        Guid practiceid = Guid.Parse(blank.PracticeId);
        Guid groupid = Guid.Parse(blank.GroupId);
        Guid practiceleadid = Guid.Parse(blank.PracticeLeadId);
        DateOnly dateStart = DateOnly.FromDateTime(blank.DateStart);
        DateOnly dateEnd = DateOnly.FromDateTime(blank.DateEnd);

        Practiceschedule? practiceschedule = _practiceRepository.GetPracticeScheduleById(id);

        practiceschedule.Practiceid = practiceid;
        practiceschedule.Groupid = groupid;
        practiceschedule.Practiceleadid = practiceleadid;
        practiceschedule.Datestart = dateStart;
        practiceschedule.Dateend = dateEnd;

        _practiceRepository.EditPracticeSchedule(practiceschedule);
    }

    private void AddSchedule(PracticeScheduleBlank blank)
    {
        Guid id = Guid.NewGuid();
        Guid practiceid = Guid.Parse(blank.PracticeId);
        Guid groupid = Guid.Parse(blank.GroupId);
        Guid practiceleadid = Guid.Parse(blank.PracticeLeadId);
        DateOnly dateStart = DateOnly.FromDateTime(blank.DateStart);
        DateOnly dateEnd = DateOnly.FromDateTime(blank.DateEnd);

        Practiceschedule newSchedule = new Practiceschedule(id, practiceid, groupid, practiceleadid, dateStart, dateEnd);
		
        _practiceRepository.AddSchedule(newSchedule);
        CreatePracticeLogForSchedule(groupid, id);
    }
}
using API.Database;
using API.Domain.group;
using API.Domain.practice.domain;
using API.Domain.shared;
using API.Domain.user;
using API.Services.company.interfaces;
using API.Services.group.interfaces;
using API.Services.practice.interfaces;
using API.Services.user.interfaces;

namespace API.Services.practice;

public class PracticeLogService : IPracticeLogService
{
    private readonly IPracticeRepository _practiceRepository;

    private IGroupService _groupService;
    private IUserService _userService;
    private ICompanyService _companyService;

    public PracticeLogService(IGroupService groupService, IUserService userService, ICompanyService companyService, IPracticeRepository practiceRepository)
    {
        _groupService = groupService;
        _userService = userService;
        _companyService = companyService;
        _practiceRepository = practiceRepository;
    }

    public PracticeLogDomain GetPracticeLogByPracticeId(string practiceId)
    {
        try
        {
            Guid id = new Guid(practiceId);

            Practice practice = _practiceRepository.GetPracticeById(id);
            Practiceschedule practiceschedule = _practiceRepository.GetPracticeScheduleByPracticeId(practice.Id);
            
            GroupDomain groupDomain = _groupService.GetGroupById(practiceschedule.Groupid);
            
            Practicelog[] practicelogItems = _practiceRepository.GetPracticeLogsByPracticescheduleId(practiceschedule.Id);
            
            Guid[] studentIds = practicelogItems.Select(log => log.Userid).ToArray();
            UserDomain[] studentDomains = _userService.GetUsersByIds(studentIds);

            Guid?[] companiesIds = practicelogItems.Select(log => log.Companyid).ToArray();
            Item[] companies = _companyService.GetCompaniesItemsByIds(companiesIds);

            PracticeLogDomain practiceLogDomain = new PracticeLogDomain(practice, practiceschedule,
                groupDomain, practicelogItems, studentDomains, companies);

            return practiceLogDomain;
        }
        catch
        {
            return new PracticeLogDomain();
        }


    }

    public PracticeScheduleDomain[] GetPracticeSchedules()
    {
        Practiceschedule[] practiceschedules = _practiceRepository.GetAllPracticeSchedules();
        
        Guid[] practiceIds = practiceschedules
            .Select(ps => ps.Practiceid)
            .ToArray();
        Guid[] groupIds = practiceschedules
            .Select(ps => ps.Groupid)
            .ToArray();
        
        Guid[] practiceLeadIds = practiceschedules
            .Select(ps => ps.Practiceleadid)
            .ToArray();
        
        
        Practice[] practices = _practiceRepository.GetPracticesByIds(practiceIds);
        GroupDomain[] groups = _groupService.GetGroupByIds(groupIds);
        UserDomain[] users = _userService.GetUsersByIds(practiceLeadIds);

        List<PracticeScheduleDomain> practiceScheduleDomains =
            PracticeScheduleDomain.ConvertFromPracticeScheduleList(practiceschedules, practices, groups, users);

        return practiceScheduleDomains.ToArray();
    }
}
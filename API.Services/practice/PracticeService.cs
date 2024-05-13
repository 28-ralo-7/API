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

public class PracticeService : IPracticeService
{
    private readonly IPracticeRepository _practiceRepository;
    private readonly IGroupService _groupService;
    private readonly IUserService _userService;
    private readonly ICompanyService _companyService;

    public PracticeService(IPracticeRepository practiceRepository, IGroupService groupService,
        IUserService userService, ICompanyService companyService)
    {
        _practiceRepository = practiceRepository;
        _groupService = groupService;
        _userService = userService;
        _companyService = companyService;
    }

    public Item[] GetPracticeItemsByPermissions(UserDomain systemUser)
    {
        Practiceschedule[] practiceSchedules = _practiceRepository.GetPracticeSchedulesByUserId(systemUser.Id);
        Guid[] practiceIds = practiceSchedules
            .Select(ps => ps.Practiceid)
            .Distinct()
            .ToArray();

        Practice[] practices = _practiceRepository.GetPracticesByIds(practiceIds);

        Item[] items = practices
            .Select(practice => new Item(practice.Id.ToString(), practice.Name))
            .ToArray();

        return items;
    }

    public PracticeLogDomain GetPracticeLogByPracticeId(string practiceId)
    {
        try
        {
            Guid id = new Guid(practiceId);

            Practice practice = _practiceRepository.GetPracticeById(id);
            Practiceschedule practiceschedule = _practiceRepository.GetPracticeSchedulesByPracticeId(practice.Id);
            
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
}
using API.Domain.adminPanel;
using API.Domain.practice.blank;
using API.Domain.practice.domain;
using API.Domain.shared;
using API.Domain.tools;
using API.Domain.user;
using API.Services.adminPanel.interfaces;
using API.Services.company.interfaces;
using API.Services.group.interfaces;
using API.Services.practice.interfaces;
using API.Services.user.interfaces;

namespace API.Services.adminPanel;

public class AdminPanelService : IAdminPanelService
{
    private readonly IUserService _userService;
    private readonly IGroupService _groupService;
    private readonly ICascadeGroupRemoveService _cascadeGroupRemoveService;
    private readonly IPracticeService _practiceService;
    private readonly ICompanyService _companyService;
    private readonly IPracticeLogService _practiceLogService;
    private readonly ISavePracticeScheduleService _practiceScheduleService;

    public AdminPanelService(IUserService userService, IGroupService groupService,
        IPracticeService practiceService, ICompanyService companyService,
        ICascadeGroupRemoveService cascadeGroupRemoveService,
        IPracticeLogService practiceLogService, ISavePracticeScheduleService practiceScheduleService)
    {
        _userService = userService;
        _groupService = groupService;
        _practiceService = practiceService;
        _companyService = companyService;
        _cascadeGroupRemoveService = cascadeGroupRemoveService;
        _practiceLogService = practiceLogService;
        _practiceScheduleService = practiceScheduleService;
    }

    public UserDomain[] GetAllUsers()
    {
        UserDomain[] users = _userService.GetAllUsers();

        return users;
    }

    public (Item[], Item[]) GetOptionsUserSetting()
    {
        Item[] roleOptions = _userService.GetRoleOptions();
        Item[] groupOptions = _groupService.GetGroupOptions();

        (Item[], Item[]) options;
        options.Item1 = roleOptions;
        options.Item2 = groupOptions;

        return options;
    }

    public Response SaveUser(UserBlank blank)
    {
        Response response = _userService.SaveUser(blank);

        return response;
    }

    public Response RemoveUser(string userId)
    {
        Response response = _userService.RemoveUser(userId);

        return response;
    }

    public Item[] GetPractices()
    {
        return _practiceService.GetAllPractices();
    }

    public Item[] GetGroups()
    {
        return _groupService.GetAllGroup();
    }

    public Item[] GetCompanies()
    {
        return _companyService.GetAllCompany();
    }

    public Response SavePractice(Item practice)
    {
        Response response = _practiceService.SavePractice(practice);

        return response;
    }

    public Response SaveGroup(Item group)
    {
        Response response = _groupService.SaveGroup(group);

        return response;
    }

    public Response SaveCompany(Item company)
    {
        Response response = _companyService.SaveCompany(company);

        return response;
    }

    public Response RemovePractice(string practiceId)
    {
        Response response = _practiceService.RemovePractice(practiceId);

        return response;
    }

    public Response RemoveGroup(string groupId)
    {
        Response response = _cascadeGroupRemoveService.RemoveGroup(groupId);

        return response;
    }

    public Response RemoveCompany(string companyId)
    {
        Response response = _companyService.RemoveCompany(companyId);

        return response;
    }

    public PracticeScheduleDomain[] GetPracticeSchedules()
    {
        PracticeScheduleDomain[] schedules = _practiceLogService.GetPracticeSchedules();

        return schedules;
    }

    public PracticeScheduleSettingsOptions GetOptionsForPracticeSchedule()
    {
        Item[] practiceOptions = _practiceService.GetAllPractices();
        Item[] practiceLeadOptions = _userService.GetAllPracticeLeadOptions();
        Item[] groupOptions = _groupService.GetGroupOptions();

        PracticeScheduleSettingsOptions options =
            new PracticeScheduleSettingsOptions(practiceOptions, practiceLeadOptions, groupOptions);

        return options;
    }

    public Response SaveSchedule(PracticeScheduleBlank blank)
    {
        Response response = _practiceScheduleService.SaveSchedule(blank);

        return response;
    }

    public Response RemoveSchedule(string scheduleId)
    {
        Response response = _practiceService.RemoveSchedule(scheduleId);

        return response;
    }
}
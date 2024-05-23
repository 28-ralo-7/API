using API.Domain.adminPanel;
using API.Domain.shared;
using API.Domain.tools;
using API.Domain.user;
using API.Services.adminPanel.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AdminPanelController : BaseController
{
    private readonly IAdminPanelService _adminPanelService;

    public AdminPanelController(IAdminPanelService adminPanelService)
    {
        _adminPanelService = adminPanelService;
    }

    [Authorize(Roles = "1")]
    [HttpGet]
    public Response GetAllUsers()
    {
        Response response = new Response();

        UserDomain[] userDomains = _adminPanelService.GetAllUsers();
        UserSettingView[] userSettingViews = userDomains
            .Select(domain => new UserSettingView(domain))
            .OrderBy(user => user.Group?.Value)
            .ThenBy(user => user.FullName)
            .ToArray();

        if (userSettingViews.Length == 0)
        {
            response.AddError("Пользователи отсутствуют");
        }
        else
        {
            response = new Response(userSettingViews);
        }

        return response;
    }
    
    [Authorize(Roles = "1")]
    [HttpGet]
    public Response GetOptionsUserSetting()
    {
        Response response = new Response();

        (Item[], Item[]) optionTuple = _adminPanelService.GetOptionsUserSetting();
        UserSettingOptions options = new UserSettingOptions(optionTuple.Item1, optionTuple.Item2);

        if (options.RoleOptions.Length == 0)
        {
            response.AddError("Роли отсутствуют");
        }
        else if (options.GroupOptions.Length == 0)
        {
            response.AddError("Группы отсутствуют");
        }
        else
        {
            response = new Response(options);
        }

        return response;
    }
    
    [Authorize(Roles = "1")]
    [HttpPost]
    public Response SaveUser(UserBlank blank)
    {
        Response response = _adminPanelService.SaveUser(blank);
        
        return response;
    }
    
    [Authorize(Roles = "1")]
    [HttpPost]
    public Response RemoveUser(string userId)
    {
        Response response = _adminPanelService.RemoveUser(userId);
        
        return response;
    }

    [Authorize(Roles = "1")]
    [HttpGet]
    public Response GetPractices()
    {
        Response response = new Response();
        Item[] practices = _adminPanelService.GetPractices();

        if (practices.Length == 0)
        {
            response.AddError("Практики отсутствуют");
        }
        else
        {
            response = new Response(practices);
        }

        return response;
    }
    
    [Authorize(Roles = "1")]
    [HttpGet]
    public Response GetGroups()
    {
        Response response = new Response();
        Item[] groups = _adminPanelService.GetGroups();

        if (groups.Length == 0)
        {
            response.AddError("Группы отсутствуют");
        }
        else
        {
            response = new Response(groups);
        }
        
        return response;
    }

    [Authorize(Roles = "1")]
    [HttpGet]
    public Response GetCompanies()
    {
        Response response = new Response();
        Item[] companies = _adminPanelService.GetCompanies();

        if (companies.Length == 0)
        {
            response.AddError("Компании отсутствуют");
        }
        else
        {
            response = new Response(companies);
        }

        return response;
    }

    [Authorize(Roles = "1")]
    [HttpPost]
    public Response SavePractice(Item practice)
    {
        Response response = _adminPanelService.SavePractice(practice);
        
        return response;
    }

    [Authorize(Roles = "1")]
    [HttpPost]
    public Response SaveGroup(Item group)
    {
        Response response = _adminPanelService.SaveGroup(group);
        
        return response;
    }

    [Authorize(Roles = "1")]
    [HttpPost]
    public Response SaveCompany(Item company)
    {
        Response response = _adminPanelService.SaveCompany(company);
        
        return response;
    }
    
    [Authorize(Roles = "1")]
    [HttpPost]
    public Response RemovePratice(string practiceId)
    {
        return _adminPanelService.RemovePratice(practiceId);
    }
    
    [Authorize(Roles = "1")]
    [HttpPost]
    public Response RemoveGroup(string groupId)
    {
        return _adminPanelService.RemoveGroup(groupId);
    }
    
    [Authorize(Roles = "1")]
    [HttpPost]
    public Response RemoveCompany(string companyId)
    {
        return _adminPanelService.RemoveCompany(companyId);
    }
}
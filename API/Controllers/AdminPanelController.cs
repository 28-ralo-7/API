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
        UserSettingView[] userSettingViews = userDomains.Select(domain => new UserSettingView(domain)).ToArray();

        if (userSettingViews == null || userSettingViews.Length == 0)
        {
            response.AddError("Пользователи отсутствуют");
        }
        else
        {
            response = new Response(userSettingViews);
        }

        return response;
    }
}
using API.Domain.shared;
using API.Domain.tools;
using API.Domain.user;
using API.Services.adminPanel.interfaces;
using API.Services.group.interfaces;
using API.Services.user.interfaces;

namespace API.Services.adminPanel;

public class AdminPanelService: IAdminPanelService
{
    private readonly IUserService _userService;
    private readonly IGroupService _groupService;

    public AdminPanelService(IUserService userService, IGroupService groupService)
    {
        _userService = userService;
        _groupService = groupService;
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
}
using API.Domain.user;
using API.Services.adminPanel.interfaces;
using API.Services.user.interfaces;

namespace API.Services.adminPanel;

public class AdminPanelService: IAdminPanelService
{
    private readonly IUserService _userService;

    public AdminPanelService(IUserService userService)
    {
        _userService = userService;
    }

    public UserDomain[] GetAllUsers()
    {
        UserDomain[] users = _userService.GetAllUsers();

        return users;
    }
}
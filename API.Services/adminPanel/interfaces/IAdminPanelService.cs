using API.Domain.shared;
using API.Domain.tools;
using API.Domain.user;

namespace API.Services.adminPanel.interfaces;

public interface IAdminPanelService
{
    UserDomain[] GetAllUsers();
    (Item[], Item[]) GetOptionsUserSetting();
    Response SaveUser(UserBlank blank);
    Response RemoveUser(string userId);
}
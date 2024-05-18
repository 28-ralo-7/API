using API.Domain.user;

namespace API.Services.adminPanel.interfaces;

public interface IAdminPanelService
{
    UserDomain[] GetAllUsers();
}
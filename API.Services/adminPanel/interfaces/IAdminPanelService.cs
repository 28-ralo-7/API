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
    Item[] GetPractices();
    Item[] GetGroups();
    Item[] GetCompanies();
    Response SavePractice(Item practice);
    Response SaveGroup(Item group);
    Response SaveCompany(Item company);
    Response RemovePratice(string practiceId);
    Response RemoveGroup(string groupId);
    Response RemoveCompany(string companyId);
}
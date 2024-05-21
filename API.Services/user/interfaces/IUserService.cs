using API.Domain.shared;
using API.Domain.tools;
using API.Domain.user;

namespace API.Services.user.interfaces
{
    public interface IUserService
    {
        UserDomain? GetUserByLoginAndPasswordHash(string login, string passwordHash);
        UserDomain[] GetUsersByIds(Guid[] ids);
        UserDomain[] GetAllUsers();
        Item[] GetRoleOptions();
        Response SaveUser(UserBlank blank);
        Response RemoveUser(string userId);
    }
}

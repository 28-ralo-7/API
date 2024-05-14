using API.Domain.tools;
using API.Domain.user;

namespace API.Services.Auth.Interfaces
{
    public interface IAuthService
    {
        Response Authorization(string login, string password);
        Response CheckAuthAndPermission(UserDomain systemUser);
    }
}

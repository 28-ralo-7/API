using API.Domain.tools;

namespace API.Services.Auth.Interfaces
{
    public interface IAuthService
    {
        Response Authorization(string login, string password);
    }
}

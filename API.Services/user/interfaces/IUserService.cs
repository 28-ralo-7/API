using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Domain.user;

namespace API.Services.user.interfaces
{
    public interface IUserService
    {
        UserDomain? GetUserByLoginAndPasswordHash(string login, string passwordHash);
        UserDomain[] GetUsersByIds(Guid[] ids);
    }
}

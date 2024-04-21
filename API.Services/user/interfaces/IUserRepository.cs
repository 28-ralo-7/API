using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.user.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserByLoginAndPasswordHash(string login, string passwordHash);
    }
}

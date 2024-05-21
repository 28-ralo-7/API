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
        User? GetUserByLogin(string login);
        User[] GetUsersByIds(Guid[] ids);
        User? GetUserById(Guid id);
        User[] GetAllUsers(bool isRemoved = false);
        Database.Role GetRole(int id);
        Database.Role[] GetAllRoles(bool isRemoved = false);
        void RemoveUser(User user);

        void EditUser(User user);
        void AddUser(User user);
    }
}

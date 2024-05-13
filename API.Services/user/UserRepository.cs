using API.Database;
using API.Services.user.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.user
{
    public class UserRepository : IUserRepository
    {
        private readonly PracticetrackerContext _context;

        public UserRepository(PracticetrackerContext context) 
        {
            _context = context;
        }

        public User? GetUserByLoginAndPasswordHash(string login, string passwordHash)
        {
            return _context.Users.FirstOrDefault(user => user.Login == login && user.Passwordhash == passwordHash && user.Isremoved != true);
        }

        public User[] GetUsersByIds(Guid[] ids)
        {
            return _context.Users
                .Where(user => ids.Contains(user.Id) && user.Isremoved != true)
                .ToArray();
        }
    }
}

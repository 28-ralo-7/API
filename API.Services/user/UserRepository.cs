using API.Database;
using API.Services.user.Interfaces;

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

        public User? GetUserByLogin(string login)
        {
            return _context.Users.FirstOrDefault(user => user.Login == login && user.Isremoved != true);
        }

        public User[] GetUsersByIds(Guid[] ids)
        {
            return _context.Users
                .Where(user => ids.Contains(user.Id) && user.Isremoved != true)
                .ToArray();
        }

        public User? GetUserById(Guid id)
        {
            return _context.Users.FirstOrDefault(user => user.Id == id && user.Isremoved != true);
        }

        public User[] GetAllUsers(bool isRemoved = false)
        {
            return _context.Users
                .Where(user => user.Isremoved == isRemoved)
                .ToArray();
        }

        public Database.Role GetRole(int id)
        {
            return _context.Roles.FirstOrDefault(role => role.Id == id);
        }

        public Database.Role[] GetAllRoles(bool isRemoved = false)
        {
            return _context.Roles.ToArray();
        }

        public void RemoveUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void RemoveUsers(User[] users)
        {
            _context.Users.UpdateRange(users);
            _context.SaveChanges();
        }

        public User[] GetUsersByGroupId(Guid groupId)
        {
            return _context.Users
                .Where(user => user.Groupid == groupId)
                .ToArray();
        }

        public void EditUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User[] GetPracticeLeads()
        {
            return _context.Users.Where(user => user.Roletype == 2)
                .ToArray();
        }
    }
}

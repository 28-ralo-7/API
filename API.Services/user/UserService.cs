using API.Services.user.interfaces;
using API.Services.user.Interfaces;
using API.Domain.group;
using API.Domain.user;
using API.Services.group.interfaces;

namespace API.Services.user
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupService _groupSevice;

        public UserService(IUserRepository userRepository, IGroupService groupSevice)
        {
            _userRepository = userRepository;
            _groupSevice = groupSevice;
        }

        public UserDomain? GetUserByLoginAndPasswordHash(string login, string passwordHash)
        {
            User? user = _userRepository.GetUserByLoginAndPasswordHash(login, passwordHash);
            
            if (user != null)
            {
                GroupDomain? groupDomain = _groupSevice.GetGroupById(user.Groupid);
                Database.Role role = _userRepository.GetRole(user.Roletype);
                UserDomain userDomain = new UserDomain(user, groupDomain, role);
            
                return userDomain; 
            }

            return null;
        }

        public UserDomain[] GetUsersByIds(Guid[] ids)
        {
            List<UserDomain> userDomains = new List<UserDomain>();
            User[] users = _userRepository.GetUsersByIds(ids);

            foreach (User user in users)
            {
                GroupDomain? groupDomain = _groupSevice.GetGroupById(user.Groupid);
                Database.Role role = _userRepository.GetRole(user.Roletype);
                UserDomain userDomain = new UserDomain(user, groupDomain, role);
                
                userDomains.Add(userDomain);
            }

            return userDomains.ToArray();
        }

        public UserDomain[] GetAllUsers()
        {
            List<UserDomain> userDomains = new List<UserDomain>();
            User[] users = _userRepository.GetAllUsers();

            foreach (User user in users)
            {
                GroupDomain? groupDomain = _groupSevice.GetGroupById(user.Groupid);
                Database.Role role = _userRepository.GetRole(user.Roletype);
                UserDomain userDomain = new UserDomain(user, groupDomain,role );
                
                userDomains.Add(userDomain);
            }

            return userDomains.ToArray();
        }
    }
}

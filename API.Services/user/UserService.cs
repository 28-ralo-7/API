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

                UserDomain userDomain = new UserDomain(user, groupDomain);
            
                return userDomain; 
            }

            return null;
        }
    }
}

using System.Security.Cryptography;
using System.Text;
using API.Database;
using API.Services.user.interfaces;
using API.Services.user.Interfaces;
using API.Domain.group;
using API.Domain.shared;
using API.Domain.tools;
using API.Domain.user;
using API.Services.group.interfaces;
using API.Services.practice.interfaces;
using API.Services.user.validate;

namespace API.Services.user
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupService _groupService;
        private readonly IPracticeService _practiceService;
        private readonly VUserService _vUserService;

        public UserService(IUserRepository userRepository, IGroupService groupService,
            VUserService userService, IPracticeService practiceService)
        {
            _userRepository = userRepository;
            _groupService = groupService;
            _vUserService = userService;
            _practiceService = practiceService;
        }

        public UserDomain? GetUserByLoginAndPasswordHash(string login, string passwordHash)
        {
            User? user = _userRepository.GetUserByLoginAndPasswordHash(login, passwordHash);
            
            if (user != null)
            {
                GroupDomain? groupDomain = _groupService.GetGroupById(user.Groupid);
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
                GroupDomain? groupDomain = _groupService.GetGroupById(user.Groupid);
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
                GroupDomain? groupDomain = _groupService.GetGroupById(user.Groupid);
                Database.Role role = _userRepository.GetRole(user.Roletype);
                UserDomain userDomain = new UserDomain(user, groupDomain,role );

                userDomains.Add(userDomain);
            }

            return userDomains.ToArray();
        }

        public Item[] GetRoleOptions()
        {
               Database.Role[] roles = _userRepository.GetAllRoles();

                Item[] options = roles
                    .Select(role => new Item(role.Id.ToString(), role.Type))
                    .ToArray();

                return options;
        }

        public Item[] GetAllPracticeLeadOptions()
        {
            User[] users = _userRepository.GetPracticeLeads();

            Item[] options = users
                .Select(user => new Item(user.Id.ToString(), (user.Surname + " " + user.Name[0] + "." + (user.Patronomic != null ? user.Patronomic[0] + "." : ""))))
                .ToArray();

            return options;
        }

        public Response SaveUser(UserBlank blank)
        {
            Response response = new Response();
            List<string> errors = _vUserService.ValidateUserBlank(blank);

            if (errors.Count != 0)
            {
                response.AddErrors(errors);
            }
            else
            {
                if (blank.Id == null)
                {
                    AddUser(blank);
                }
                else
                {
                    EditUser(blank);
                }
            }

            return response;
        }

        public Response RemoveUser(string userId)
        {
            Guid id = Guid.Parse(userId);
            User user = _userRepository.GetUserById(id);
            user.Isremoved = true;

            if (user != null && user.Id != null)
            {
                _userRepository.RemoveUser(user);
            }

            return new Response();
        }

        public Response RemoveUsersByGroup(Guid groupId)
        {
            User[] users = _userRepository.GetUsersByGroupId(groupId);
            Guid[] userIds = users
                .Select(user => user.Id)
                .ToArray();
            
            for (int i = 0; i < users.Length; i++)
            {
                users[i].Isremoved = true;
            }
            
            _practiceService.RemovePracticeLogsByUserIds(userIds);
            _userRepository.RemoveUsers(users);

            return new Response();
        }

        public User[] GetStudentsByGroupId(Guid groupId)
        {
            return _userRepository.GetUsersByGroupId(groupId);
        }

        private void AddUser(UserBlank blank)
        {
            Guid id = Guid.NewGuid();
            int roleType = Int32.Parse(blank.RoleId);
            string passwordHash = GetPasswordHash(blank.Password);
            Guid? groupId = null;

            if (blank.RoleId == "3")
            {
                groupId = Guid.Parse(blank.GroupId);
            }

            User user = new User(id, blank.Surname, blank.Name, blank.Patronymic, blank.Login, passwordHash, roleType, groupId, false);
            
            _userRepository.AddUser(user);
        }

        private void EditUser(UserBlank blank)
        {
            Guid id = Guid.Parse(blank.Id);
            int roleType = Int32.Parse(blank.RoleId);
            
            User user = _userRepository.GetUserById(id);

            user.Surname = blank.Surname.Trim();
            user.Name = blank.Name.Trim();
            user.Patronomic = blank.Patronymic?.Trim();
            user.Login = blank.Login.Trim();
            user.Roletype = roleType;

            if (blank.RoleId == "3")
            {
                Guid groupId = Guid.Parse(blank.GroupId);
                user.Groupid = groupId;
            }

            if (blank.Password != null && !String.IsNullOrWhiteSpace(blank.Password))
            {
                string passwordHash = GetPasswordHash(blank.Password);

                user.Passwordhash = passwordHash;
            }
            
            _userRepository.EditUser(user);

        }
        
        private static string GetPasswordHash(string password)
        {
            Byte[] bytes = Encoding.Unicode.GetBytes(password);
            MD5CryptoServiceProvider cryptoService = new MD5CryptoServiceProvider();
            Byte[] byteHash = cryptoService.ComputeHash(bytes);
            String hash = String.Empty;

            foreach (Byte b in byteHash)
                hash += String.Format("{0:x2}", b);

            return hash;
        }
    }
}

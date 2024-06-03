using API.Domain.user;
using API.Services.user.Interfaces;

namespace API.Services.user.validate;

public class VUserService
{
    private readonly IUserRepository _userRepository;

    public VUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public List<string> ValidateUserBlank(UserBlank blank)
    {
        List<string> errors = new List<string>();

        if (String.IsNullOrWhiteSpace(blank.Surname))
        {
            errors.Add("Заполните поле фамилия");
        }
        if (String.IsNullOrWhiteSpace(blank.Name))
        {
            errors.Add("Заполните поле имя");
        }
        if (String.IsNullOrWhiteSpace(blank.Login))
        {
            errors.Add("Заполните поле логин");
        }
        if (String.IsNullOrWhiteSpace(blank.Password) && blank.Id == null)
        {
            errors.Add("Заполните поле пароль");
        }
        if (String.IsNullOrWhiteSpace(blank.RoleId))
        {
            errors.Add("Выберите роль пользователя");
        }
        if (String.IsNullOrWhiteSpace(blank.GroupId) && blank.RoleId == "3")
        {
            errors.Add("Выберите группу для студента");
        }

        if (errors.Count == 0)
        {
            User? user = _userRepository.GetUserByLogin(blank.Login);
            string blankId = blank.Id ?? "";
            if (user != null && user?.Id != null && user.Id.ToString() != blankId.Trim())
            {
                errors.Add("Пользователь с таким логином уже есть");
            }
        }

        return errors;
    }
}
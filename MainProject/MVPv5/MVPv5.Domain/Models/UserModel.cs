using System.ComponentModel.DataAnnotations;
using MVPv5.Domain.Entities;

namespace MVPv5.Domain.Models;

public class UserModel
{
    private UserModel(UserEntity entity)
    {
        Id = entity.Id;
        Nickname = entity.Nickname;
        Login = entity.Login;
        Password = entity.Password;
        AccessRule = entity.AccessRule;
        DateCreation = entity.DateCreation;
    }
    private UserModel(string nickname, string login, string password, byte accessRule, DateOnly? dateCreation)
    {
        Nickname = nickname;
        Login = login;
        Password = password;
        AccessRule = accessRule;
        DateCreation = dateCreation ?? DateOnly.FromDateTime(DateTime.Now);
    }
    public int Id { get; }
    public string Nickname { get; }
    public string Login { get; }
    public string Password { get; }
    public byte AccessRule { get; }
    public DateOnly DateCreation { get; }

    public static (UserModel User, string Error) Create(UserEntity? entity)
    {
        if (entity is null)
        {
            throw new KeyNotFoundException("Пользователь не найден");
        }

        var error = string.Empty;

        if (string.IsNullOrEmpty(entity.Nickname) || string.IsNullOrEmpty(entity.Login) || string.IsNullOrEmpty(entity.Password))
        {
            error = "Никнейм, логин или пароль пусты";
        }

        return (new UserModel(entity), error);
    }
    public static UserModel Create(string nickname, string login, string password, byte accessRule, DateOnly? dateCreation)
    {
        if (string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
        {
            throw new ValidationException("Ошибка валидации");
        }
        return new UserModel(nickname, login, password, accessRule, dateCreation);
    }
}

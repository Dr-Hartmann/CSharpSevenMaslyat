namespace MVPv5.Core.Models;

public class UserModel
{
    private UserModel(int id, string nickname, string login, string password,
        byte accessRule, DateOnly dateCreation, IEnumerable<DocumentModel>? documents)
    {
        Id = id;
        Nickname = nickname;
        Login = login;
        Password = password;
        AccessRule = accessRule;
        DateCreation = dateCreation;
        Documents = documents?.Select(i => new DocumentModel() { UserId = i.UserId, User = this });
    }
    public int Id { get; }
    public string Nickname { get; }
    public string Login { get; }
    public string Password { get; }
    public byte AccessRule { get; }
    public DateOnly DateCreation { get; }
    public IEnumerable<DocumentModel>? Documents { get; }

    public static (UserModel, string) Create(int id, string nickname, string login, string password,
        byte accessRule, DateOnly dateCreation, IEnumerable<DocumentModel>? documents)
    {
        var error = string.Empty;

        if (string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
        {
            error = "Никнейм, логин или пароль пусты";
        }

        return (new UserModel(id, nickname, login, password, accessRule, dateCreation, documents), error);
    }
}

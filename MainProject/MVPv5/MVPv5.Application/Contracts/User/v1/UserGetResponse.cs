namespace MVPv5.Application.Contracts.User.v1;

public class UserGetResponse
{
    public int Id { get; set; }
    public string? Nickname { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public byte AccessRule { get; set; }
    public DateOnly DateCreation { get; set; }
}


//[Remote(action: "CheckEmail", controller: "DocumentV1", ErrorMessage = "Имя уже используется")]
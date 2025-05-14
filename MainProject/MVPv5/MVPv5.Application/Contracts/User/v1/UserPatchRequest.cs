using System.ComponentModel.DataAnnotations;

namespace MVPv5.Application.Contracts.User.v1;

public class UserPatchRequest
{
    [Required]
    public int Id { get; set; }

    [StringLength(20, MinimumLength = 2)]
    public string? Nickname { get; set; }

    [StringLength(50, MinimumLength = 10)]
    public string? Login { get; set; }

    [StringLength(30, MinimumLength = 5)]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string? PasswordConfirm { get; set; }

    public byte? AccessRule { get; set; }

    public DateOnly? DateCreation { get; set; }
}


//[Remote(action: "CheckEmail", controller: "DocumentV1", ErrorMessage = "Имя уже используется")]
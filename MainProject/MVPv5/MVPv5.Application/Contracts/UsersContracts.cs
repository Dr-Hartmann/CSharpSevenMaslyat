using MVPv5.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace MVPv5.Application.Contracts;

public record class UserCreateRequest
{
    [Required, StringLength(20, MinimumLength = 2)]
    public string Nickname { get; set; } = string.Empty;

    // TODO - почта ЧГУ регулярка
    // '^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+[.][A-Za-z]+$
    [Required, StringLength(50, MinimumLength = 10)]
    public string Login { get; set; } = string.Empty;

    [Required, StringLength(30, MinimumLength = 5)]
    public string Password { get; set; } = string.Empty;

    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string PasswordConfirm { get; set; } = string.Empty;
}

public class UserGetResponse
{
    public int Id { get; set; }
    public string? Nickname { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public byte AccessRule { get; set; }
    public DateOnly DateCreation { get; set; }
    public IEnumerable<DocumentModel>? Documents { get; set; }
}

public class UserPatchRequest
{
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
    //public IEnumerable<DocumentModel>? Documents { get; set; }
}


//[Remote(action: "CheckEmail", controller: "DocumentV1", ErrorMessage = "Имя уже используется")]
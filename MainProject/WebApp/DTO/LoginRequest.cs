using System.ComponentModel.DataAnnotations;

namespace DTOmvp;

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}



/* TODO
 * На заметку следующие аннотации
 */



//[Remote(action: "CheckEmail", controller: "DocumentV1", ErrorMessage = "Имя уже используется")]

//[Required]
//public string? Password { get; set; }

//[Compare("Password", ErrorMessage = "Пароли не совпадают")]
//public string? PasswordConfirm { get; set; }

//[ForeignKey("Name")]
//public int DocumentId { get; set; }
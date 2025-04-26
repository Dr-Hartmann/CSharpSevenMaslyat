using System.ComponentModel.DataAnnotations;

namespace MVPv5.Core.Models;

public class User
{
    [Key]
    public int Id { get; set; } = 1;

    //[Required, MaxLength(50)]
    public string? Nickname { get; set; }

    [Required]
    [MaxLength(50)]
    // почта ЧГУ регулярка
    // '^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+[.][A-Za-z]+$'
    public string? Login { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Password { get; set; }

    //[Compare("Password", ErrorMessage = "Пароли не совпадают")]
    //public string? PasswordConfirm { get; set; }

    public byte AccessRule { get; set; }

    // коллекция документов
}



//[Remote(action: "CheckEmail", controller: "DocumentV1", ErrorMessage = "Имя уже используется")]

//[ForeignKey("Name")]
//public int DocumentId { get; set; }

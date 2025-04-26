using System.ComponentModel.DataAnnotations;

namespace MVPv5.Core;

public class RegistrationRequest
{
    [Required(ErrorMessage = "Не указан Логин")]
    public string? Login { get; set; }
    [Required(ErrorMessage = "Не указан Пароль")]
    public string? Password { get; set; }
}

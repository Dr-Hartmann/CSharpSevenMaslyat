using System.ComponentModel.DataAnnotations;

namespace MVPv5.API.Entities;

public class UserEntity
{
    [Key]
    public int Id { get; set; }
    public string? Nickname { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public char AccessRule { get; set; }

    // коллекция документов
}

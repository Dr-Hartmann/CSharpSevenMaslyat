using System.ComponentModel.DataAnnotations;

namespace MVPv5.Domain.Entities;

public class UserEntity
{
    [Key]
    public int Id { get; set; }
#pragma warning disable CS8618
    public string Nickname { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
#pragma warning restore CS8618
    public byte AccessRule { get; set; }
    public DateOnly DateCreation { get; set; }
}

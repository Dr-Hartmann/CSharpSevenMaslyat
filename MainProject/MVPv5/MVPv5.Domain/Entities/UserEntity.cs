using System.ComponentModel.DataAnnotations;

namespace MVPv5.Domain.Entities;

public class UserEntity
{
    [Key]
    public int Id { get; set; }
    public required string Nickname { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
    public byte AccessRule { get; set; }
    public required DateOnly DateCreation { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace MVPv5.Domain.Entities;

public class UserEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Nickname { get; set; }

    [Required]
    public required string Login { get; set; }

    [Required]
    public required string Password { get; set; }

    [Required]
    public byte AccessRule { get; set; }

    public DateOnly DateCreation { get; set; }
}

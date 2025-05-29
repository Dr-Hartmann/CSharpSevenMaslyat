using System.ComponentModel.DataAnnotations;

namespace MVPv5.Domain.Entities;

public class TemplateEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    public string? Type { get; set; }

    public DateOnly DateCreation { get; set; }

    [Required]
    public required byte[] Content { get; set; }

    [Required]
    public required string ContentType { get; set; }

    [Required]
    public required string[] Tags { get; set; }
}

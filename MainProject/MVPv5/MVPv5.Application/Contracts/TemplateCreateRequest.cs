using System.ComponentModel.DataAnnotations;

namespace MVPv5.Application.Contracts;

public class TemplateCreateRequest
{
    [Required, MaxLength(100)]
    public required string Name { get; set; }

    [Required]
    public required string Type { get; set; }

    [Required]
    public required byte[] Content { get; set; }
}

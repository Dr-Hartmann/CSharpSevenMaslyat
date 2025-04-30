using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MVPv5.Application.Contracts;

public class DocumentCreateRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int TemplateId { get; set; }

    public string? JsonData { get; set; }

    public Dictionary<string, object>? Metadata { get; set; }
}

public class DocumentGetResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int TemplateId { get; set; }
    public string? JsonData { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
    public int UserId { get; set; }
} 
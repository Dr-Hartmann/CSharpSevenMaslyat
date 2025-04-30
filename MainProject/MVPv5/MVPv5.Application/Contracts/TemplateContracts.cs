using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MVPv5.Application.Contracts;

public class TemplateCreateRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public string? FilePath { get; set; }
    // public byte[]? FileData { get; set; }

    public Dictionary<string, object>? Metadata { get; set; }
}

public class TemplateGetResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? FilePath { get; set; }
    // public byte[]? FileData { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
} 
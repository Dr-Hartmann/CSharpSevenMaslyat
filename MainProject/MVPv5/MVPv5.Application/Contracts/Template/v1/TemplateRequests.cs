using System.ComponentModel.DataAnnotations;

namespace MVPv5.Application.Contracts.Template.v1;

public record TemplateCreateRequest
{
    [Required, MaxLength(100)]
    public required string Name { get; set; }

    public string? Type { get; set; }

    [Required]
    public required byte[] Content { get; set; }

    [Required]
    public required string ContentType { get; set; }

    [Required]
    public required IEnumerable<string> Tags { get; set; }
}

public record TemplatePatchRequest
{
    [Required]
    public int Id { get; init; }

    public string? Name { get; set; }
    public string? Type { get; set; }
    public byte[]? Content { get; set; }
    public string? ContentType { get; set; }
    public IEnumerable<string>? Tags { get; set; }
}

public record TemplateUpdateRequest
{
    [Required]
    public int Id { get; init; }

    [Required, MaxLength(100)]
    public required string Name { get; set; }

    public string? Type { get; set; }

    [Required]
    public required byte[] Content { get; set; }

    [Required]
    public required string ContentType { get; set; }

    [Required]
    public required IEnumerable<string> Tags { get; set; }
}

public record TemplateDeleteRequest
{
    [Required]
    public int Id { get; init; }
}

public record TemplateDownloadRequest(
    [Required] string name,
    [Required] byte[] content,
    [Required] string contentType);

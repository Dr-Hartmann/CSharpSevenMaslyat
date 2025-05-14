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

// TODO
public record TemplateUpdateRequest();
//public record TemplateDeleteRequest();

public record TemplateDownloadRequest(
    [Required] string name,
    [Required] byte[] content,
    [Required] string contentType);

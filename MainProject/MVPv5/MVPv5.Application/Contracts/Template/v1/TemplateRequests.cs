using System.ComponentModel.DataAnnotations;

namespace MVPv5.Application.Contracts.Template.v1;

public class TemplateCreateRequest
{
    [Required, MaxLength(100)]
    public string? Name { get; set; }

    public string? Type { get; set; }

    [Required]
    public byte[]? Content { get; set; }

    [Required]
    public string? ContentType { get; set; }

    public IEnumerable<string>? Tags { get; set; }
}

public class TemplatePatchRequest
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string? Name { get; set; }

    public string? Type { get; set; }

    [Required]
    public byte[]? Content { get; set; }

    [Required]
    public string? ContentType { get; set; }
    public IEnumerable<string>? Tags { get; set; }
}

//public record TemplateUpdateRequest
//{
//    [Required]
//    public int Id { get; init; }

//    [Required, MaxLength(100)]
//    public required string Name { get; set; }

//    public string? Type { get; set; }

//    [Required]
//    public required byte[] Content { get; set; }

//    [Required]
//    public required string ContentType { get; set; }

//    [Required]
//    public required IEnumerable<string> Tags { get; set; }
//}

public record TemplateDeleteRequest
{
    [Required]
    public int Id { get; init; }
}

public record TemplateDownloadRequest(
    [Required] string Name,
    [Required] byte[] Content,
    [Required] string ContentType);

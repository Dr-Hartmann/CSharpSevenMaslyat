using System.ComponentModel.DataAnnotations;

namespace MVPv5.Application.Contracts.Document.v1;

public class DocumentCreateRequest
{
    [Required]
    public string? Name { get; set; }

    public IDictionary<string, string>? Data { get; set; }

    [Required]
    public int TemplateId { get; set; }

    [Required]
    public int UserId { get; set; }
}

public class DocumentUpdateRequest
{
    [Required]
    public required int Id { get; set; }

    public string? Name { get; set; }
    public DateOnly? DateCreation { get; set; }
    public IDictionary<string, string>? Data { get; set; }
    public int? TemplateId { get; set; }
    public int? UserId { get; set; }
}

public class DocumentPatchNameRequest
{
    [Required]
    public required int Id { get; set; }

    [Required]
    public required string Name { get; set; }
}

public class DocumentPatchMetadataRequest
{
    [Required]
    public required int Id { get; set; }

    [Required]
    public required IDictionary<string, string>? Data { get; set; }
}

public class DocumentDeleteRequest
{
    [Required]
    public required int Id { get; set; }
}

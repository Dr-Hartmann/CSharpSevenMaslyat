using System.ComponentModel.DataAnnotations;

namespace MVPv5.Application.Contracts.Document.v1;

public class DocumentBuildRequest
{
    [Required, MaxLength(30)]
    public required string Name { get; set; }

    [Required]
    public required byte[] Content { get; set; }

    [Required]
    public required IDictionary<string, string> Data { get; set; }
}

public class DocumentCreateRequest
{
    public string Name { get; set; } = string.Empty;
    public DateOnly DateCreation { get; set; }
    public required string MetadataJson { get; set; }
    public int TemplateId { get; set; }
    public int UserId { get; set; }
}

public class DocumentUpdateRequest
{
    [Required]
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateOnly DateCreation { get; set; }
    public required string MetadataJson { get; set; }
    public int TemplateId { get; set; }
    public int UserId { get; set; }
}

public class DocumentPatchNameRequest
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public class DocumentPatchMetadataRequest
{
    public int Id { get; set; }
    public required string MetadataJson { get; set; }
}

public class DocumentDeleteRequest
{
    public int Id { get; set; }
}
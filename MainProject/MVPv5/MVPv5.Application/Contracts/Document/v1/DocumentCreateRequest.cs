using System.ComponentModel.DataAnnotations;

namespace MVPv5.Application.Contracts.Document.v1;

public class DocumentCreateRequest
{
    [Required, MaxLength(30)]
    public required string Name { get; set; }

    [Required]
    public required int TemplateId { get; set; }

    //public JsonDocument? MetadataJson { get; set; }

    [Required]
    public required int UserId { get; set; }
}

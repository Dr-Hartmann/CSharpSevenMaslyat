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

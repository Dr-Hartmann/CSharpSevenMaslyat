using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace MVPv5.Domain.Entities;

public class DocumentEntity : IDisposable
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required DateOnly DateCreation { get; set; }

    [Column(TypeName = "jsonb")]
    public JsonDocument? MetadataJson { get; set; }

    [ForeignKey(nameof(TemplateEntity))]
    public required int TemplateId { get; set; }
    public TemplateEntity? Template { get; set; }

    [ForeignKey(nameof(UserEntity))]
    public required int UserId { get; set; }
    public UserEntity? User { get; set; } 

    public void Dispose() => MetadataJson?.Dispose();
}

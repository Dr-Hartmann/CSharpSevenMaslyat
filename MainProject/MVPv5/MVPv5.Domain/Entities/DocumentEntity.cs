using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVPv5.Domain.Entities;

public class DocumentEntity
{
    [Key]
    public int Id { get; set; }

    /*  TODO  */
    /// <summary>
    /// Сериализованные метаданные документа (JSON)
    /// </summary>
    public string? MetadataJson { get; set; }

    [ForeignKey(nameof(UserEntity))]
    public int UserId { get; set; }
}

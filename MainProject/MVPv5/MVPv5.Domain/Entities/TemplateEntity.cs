using System.ComponentModel.DataAnnotations;

namespace MVPv5.Domain.Entities;

public class TemplateEntity
{
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Сериализованные метаданные шаблона (JSON)
    /// </summary>
    public string? MetadataJson { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace MVPv5.Domain.Entities;

public class TemplateEntity
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public required DateOnly DateCreation { get; set; }
    public required byte[] Content { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace MVPv4.Models;

public class DocumentFile
{
    [Key]
    public int Id { get; set; }
    [Required]  
    public byte[]? File { get; set; }
}

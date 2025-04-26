using System.ComponentModel.DataAnnotations;

namespace WebAPI_ASPNET_Core.Models
{
    public class UserDocument
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int TemplateId { get; set; }  // Связь с шаблоном
        public Template? Template { get; set; }

        public string? JsonData { get; set; } // JSON с данными для заполнения шаблона

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
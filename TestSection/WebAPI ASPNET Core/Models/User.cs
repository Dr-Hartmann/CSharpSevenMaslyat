using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI_ASPNET_Core.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nickname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Login { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        public ICollection<UserDocument> UserDocuments { get; set; } = new List<UserDocument>(); // Список Id документов пользователя
    }
}
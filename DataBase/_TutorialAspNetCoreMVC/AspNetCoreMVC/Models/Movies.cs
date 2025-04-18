using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreMVC.Models
{
    public class Movies
    {
        public int ID { get; set; }

        public string? Title { get; set; }
        public string? Some { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateOnly ReleaseDate { get; set; }
        public string? Genre { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}

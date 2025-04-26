using System.ComponentModel.DataAnnotations;

namespace AspNetCoreMVC.DTO
{
    /// <summary>
    /// Data Transfer Object (DTO) для передачи данных о фильме между слоями приложения.
    /// DTO используется для:
    /// 1. Отделения внутренней модели данных от API
    /// 2. Контроля над тем, какие данные передаются клиенту
    /// 3. Предотвращения циклических зависимостей при сериализации
    /// </summary>
    public class MovieDto
    {
        /// <summary>
        /// Уникальный идентификатор фильма
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название фильма
        /// </summary>
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Дата выпуска фильма
        /// </summary>
        [Required]
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Жанр фильма
        /// </summary>
        [Required]
        [StringLength(30)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        public string Genre { get; set; } = string.Empty;

        /// <summary>
        /// Цена билета на фильм
        /// </summary>
        [Required]
        [Range(1, 100)]
        public decimal Price { get; set; }
    }
} 
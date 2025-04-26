using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreMVC.Models
{
    /// <summary>
    /// Модель данных для фильмов.
    /// Представляет собой сущность, которая будет храниться в базе данных.
    /// </summary>
    public class Movies
    {
        /// <summary>
        /// Уникальный идентификатор фильма.
        /// По соглашению об именовании в Entity Framework Core, свойство с именем ID или [ClassName]Id
        /// автоматически становится первичным ключом в базе данных.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Название фильма.
        /// Атрибут [StringLength] ограничивает максимальную длину строки до 60 символов.
        /// Атрибут [Required] указывает, что это поле обязательно для заполнения.
        /// </summary>
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Дата выпуска фильма.
        /// Атрибут [Display] задает отображаемое имя поля в пользовательском интерфейсе.
        /// Атрибут [DataType] указывает тип данных (в данном случае - только дата).
        /// </summary>
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Жанр фильма.
        /// Атрибут [RegularExpression] задает регулярное выражение для валидации.
        /// В данном случае разрешены только буквы, пробелы и апострофы.
        /// Атрибут [Required] указывает, что это поле обязательно для заполнения.
        /// Атрибут [StringLength] ограничивает максимальную длину строки до 30 символов.
        /// </summary>
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        [StringLength(30)]
        public string Genre { get; set; } = string.Empty;

        /// <summary>
        /// Цена билета на фильм.
        /// Атрибут [Range] задает допустимый диапазон значений (от 1 до 100).
        /// Атрибут [DataType] указывает тип данных (в данном случае - валюта).
        /// Атрибут [Column] задает тип данных в базе данных (decimal(18, 2)).
        /// </summary>
        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}

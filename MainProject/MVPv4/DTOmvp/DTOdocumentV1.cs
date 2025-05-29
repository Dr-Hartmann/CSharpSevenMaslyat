using System.ComponentModel.DataAnnotations;

namespace DTOmvp;

public class DTOdocumentV1
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Не указано имя документа")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина должна быть в диапазоне от 3 до 20")]
    [RegularExpression(@"^[A-Za-zА-Яа-яёЁ0-9_]+$", ErrorMessage = "Должны быть только буквы, цифры или '_'")]
    public string? Name { get; set; }
    public byte[]? File { get; set; }
    public DateOnly? Year { get; set; }
    public string? Title { get; set; }
    public string? Topic { get; set; }
    public string? Annotation { get; set; }
}

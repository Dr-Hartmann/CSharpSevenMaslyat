using System.ComponentModel.DataAnnotations;

namespace DTOmvp;

public class DTOdocumentV1
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Имя документа обязательно")]
    [RegularExpression(@"^[A-Za-zА-Яа-яёЁ0-9_]+$", ErrorMessage = "Имя может содержать только буквы, цифры и '_'")]
    public string? Name { get; set; }
    public byte[]? File { get; set; }

    [Range(1900, 2050, ErrorMessage = "Год должен быть в диапазоне от 1900 до 2050")]
    public DateOnly? Year { get; set; }
    public string? Title { get; set; }
    public string? Topic { get; set; }
    public string? Annotation { get; set; }
}

﻿using System.ComponentModel.DataAnnotations;

namespace MVPv4.Models;

public class DocumentV1
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Не указано имя документа")]
    [RegularExpression(@"^[A-Za-zА-Яа-яёЁ0-9_]+$", ErrorMessage = "Должны быть только буквы, цифры или '_'")]
    public string? Name { get; set; }

    //[Required(ErrorMessage = "Не указан шаблон документа!")]
    public byte[]? File { get; set; }

    [Range(1900, 2050, ErrorMessage = "Год должен быть в диапазоне от 1900 до 2050")]
    //[RegularExpression(@"^\d{4}$", ErrorMessage = "Неверный формат года")]
    public DateOnly? Year { get; set; }

    public string? Title { get; set; }
    
    public string? Topic { get; set; }

    public string? Annotation { get; set; }
}



/* TODO
 * На заметку следующие аннотации
 */



//[Remote(action: "CheckEmail", controller: "DocumentV1", ErrorMessage = "Имя уже используется")]

//[Required]
//public string? Password { get; set; }

//[Compare("Password", ErrorMessage = "Пароли не совпадают")]
//public string? PasswordConfirm { get; set; }

//[ForeignKey("Name")]
//public int DocumentId { get; set; }
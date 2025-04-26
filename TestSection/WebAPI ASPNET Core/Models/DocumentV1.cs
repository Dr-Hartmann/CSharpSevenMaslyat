using System.ComponentModel.DataAnnotations;

namespace WebAPI_ASPNET_Core.Models;

/// <summary>
/// Представляет сущность документа с версией 1 структуры
/// Этот класс используется для хранения метаданных и содержимого документа
/// </summary>
public class DocumentV1
{
    // Первичный ключ для документа
    // [Key] указывает Entity Framework Core, что это свойство является первичным ключом
    // Автоматически генерируется базой данных при создании новой записи
    [Key]
    public int Id { get; set; }

    // Имя документа с правилами валидации:
    // - Обязательное поле
    // - Длина от 3 до 20 символов
    // - Разрешены только буквы, цифры и символ подчеркивания
    // [Required] проверяет, что поле не пустое
    // [StringLength] ограничивает длину строки
    // [RegularExpression] проверяет соответствие строки регулярному выражению
    [Required(ErrorMessage = "Не указано имя документа")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина должна быть в диапазоне от 3 до 20")]
    [RegularExpression(@"^[A-Za-zА-Яа-яёЁ0-9_]+$", ErrorMessage = "Должны быть только буквы, цифры или '_'")]
    public string? Name { get; set; }

    // Бинарное содержимое файла документа
    // byte[] используется для хранения бинарных данных файла
    // ? указывает, что свойство может быть null
    //[Required(ErrorMessage = "Не указан шаблон документа!")]
    public byte[]? File { get; set; }

    // Год документа (валидация закомментирована)
    // DateOnly представляет только дату без времени
    // ? указывает, что свойство может быть null
    //[RegularExpression(@"^\d{4}$", ErrorMessage = "Неверный формат года")]
    public DateOnly? Year { get; set; }

    // Заголовок документа
    // string? указывает, что свойство может быть null
    public string? Title { get; set; }
    
    // Тема документа
    public string? Topic { get; set; }

    // Аннотация/описание документа
    public string? Annotation { get; set; }
}

// Закомментированные примеры валидации и связей:
//[Remote(action: "CheckEmail", controller: "DocumentV1", ErrorMessage = "Имя уже используется")]
//[Required]
//public string? Password { get; set; }
//[Compare("Password", ErrorMessage = "Пароли не совпадают")]
//public string? PasswordConfirm { get; set; }
//[ForeignKey("Name")]
//public int DocumentId { get; set; }


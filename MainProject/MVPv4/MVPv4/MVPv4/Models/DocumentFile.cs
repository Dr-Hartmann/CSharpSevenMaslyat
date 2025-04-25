using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;

namespace MVPv4.Models;

/// <summary>
/// Представляет сущность файла документа в базе данных
/// Этот класс используется для хранения бинарных данных файла в базе данных
/// </summary>
public class DocumentFile
{
    // Первичный ключ для файла документа
    // [Key] указывает Entity Framework Core, что это свойство является первичным ключом
    // Автоматически генерируется базой данных при создании новой записи
    [Key]
    public int Id { get; set; }

    // Обязательное свойство, которое хранит бинарное содержимое файла
    // [Required] гарантирует, что это поле не может быть null
    // byte[] используется для хранения бинарных данных файла в базе данных
    [Required]  
    public byte[] File { get; set; }
}

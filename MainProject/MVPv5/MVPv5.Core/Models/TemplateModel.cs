namespace MVPv5.Core.Models;

public class TemplateModel
{
    //[Key]
    //public int Id { get; set; }

    //[Required]
    //[MaxLength(100)]
    //public string Name { get; set; }

    //// Хранить путь к файлу или сам файл в виде byte[]
    //public string? FilePath { get; set; }// Если храним файл на диске
    ////public byte[]? FileData { get; set; } // Если храним файл в БД

    /// <summary>
    /// Дополнительные метаданные шаблона (параметры, пользовательские поля и т.д.)
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = new();
}

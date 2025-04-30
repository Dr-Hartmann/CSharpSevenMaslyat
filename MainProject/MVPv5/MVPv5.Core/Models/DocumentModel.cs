namespace MVPv5.Core.Models;

public class DocumentModel
{
    //[Key]
    //public int Id { get; set; }

    //[Required]
    //[MaxLength(100)]
    //public string Name { get; set; }

    //public int TemplateId { get; set; }  // Связь с шаблоном
    //public TemplateModel? Template { get; set; }

    //public string? JsonData { get; set; } // JSON с данными для заполнения шаблона

    //public ICollection<UserModel> Users { get; set; } = new List<UserModel>();

    public int UserId { get; set; }
    public UserModel? User { get; set; }

    /// <summary>
    /// Дополнительные метаданные документа (пользовательские поля, параметры, теги и т.д.)
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = new();
}

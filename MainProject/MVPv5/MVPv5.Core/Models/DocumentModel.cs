namespace MVPv5.Core.Models;

public class DocumentModel
{
    private DocumentModel(int id, string name, int templateId, int userId, DateOnly dateCreation)
    {
        Id = id;
        Name = name;
        TemplateId = templateId;
        UserId = userId;
        DateCreation = dateCreation;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly DateCreation { get; set; }
    //public JsonDocument? MetadataJson { get; set; }
    public int TemplateId { get; set; }
    public int UserId { get; set; }

    public static (DocumentModel, string) Create(int id, string name, int templateId, int userId, byte accessRule, DateOnly dateCreation)
    {
        var error = string.Empty;

        //if (string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
        //{
        //    error = "Никнейм, логин или пароль пусты";
        //}

        return (new DocumentModel(id, name, templateId, userId, dateCreation), error);
    }
}

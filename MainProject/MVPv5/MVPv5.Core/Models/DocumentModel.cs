using System.Text.Json;

namespace MVPv5.Core.Models;

public class DocumentModel
{
    private DocumentModel(int id, string name, int templateId, int userId, DateOnly dateCreation,
        JsonDocument? metadataJson)
    {
        Id = id;
        Name = name;
        TemplateId = templateId;
        UserId = userId;
        DateCreation = dateCreation;
        MetadataJson = metadataJson;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly DateCreation { get; set; }
    public JsonDocument? MetadataJson { get; set; }
    public int TemplateId { get; set; }
    public int UserId { get; set; }

    public static (DocumentModel, string) Create(int id, string name, int templateId, int userId, DateOnly dateCreation, JsonDocument metadataJson)
    {
        var error = string.Empty; //зачем в данном случае это???
        return (new DocumentModel(id, name, templateId, userId, dateCreation, metadataJson), error);
    }
}

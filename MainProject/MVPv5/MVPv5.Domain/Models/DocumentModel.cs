using MVPv5.Domain.Entities;
using System.Text.Json;

namespace MVPv5.Domain.Models;

public class DocumentModel
{
    private DocumentModel(DocumentEntity entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        TemplateId = entity.TemplateId;
        UserId = entity.UserId;
        DateCreation = entity.DateCreation;
        Dictionary = entity.MetadataJson?.Deserialize<IDictionary<string, string>>();
    }
    private DocumentModel(string? name, DateOnly? dateCreation, IDictionary<string, string>? dictionary, int? templateId, int? userId)
    {
        Name = name ?? string.Empty;
        DateCreation = dateCreation ?? DateOnly.FromDateTime(DateTime.Now);
        Dictionary = dictionary;
        TemplateId = templateId ?? default;
        UserId = userId ?? default;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly DateCreation { get; set; }
    public IDictionary<string, string>? Dictionary { get; set; }
    public int TemplateId { get; set; }
    public int UserId { get; set; }

    public static (DocumentModel Document, string Error) Create(DocumentEntity? entity)
    {
        if (entity is null)
        {
            throw new Exception("Передана пустая сущность");
        }

        var error = string.Empty;
        // TODO - добавить ошибки?
        return (new DocumentModel(entity), error);
    }
    public static DocumentModel Create(string? name, DateOnly? dateCreation, IDictionary<string, string>? dictionary, int? templateId, int? userId)
    {
        // TODO - добавить обработку ошибок 
        return new DocumentModel(name, dateCreation, dictionary, templateId, userId);
    }
}

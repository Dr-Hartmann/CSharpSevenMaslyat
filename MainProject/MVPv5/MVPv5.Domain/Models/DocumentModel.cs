using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using MVPv5.Domain.Entities;

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
            throw new ValidationException("Передана пустая сущность");
        }

        if (string.IsNullOrEmpty(entity.Name))
        {
            throw new ValidationException("Имя документа не должно быть пустым");
        }

        if(entity.MetadataJson is null)
        {
            throw new ValidationException("Документ отсутствует");
        }

        if (entity.TemplateId <= 0)
        {
            throw new ValidationException("Некорректный TemplateId");
        }

        if (entity.UserId <= 0)
        {
            throw new ValidationException("Некорректный UserId");
        }
        var error = string.Empty;
        return (new DocumentModel(entity), error);
    }
    public static DocumentModel Create(string? name, DateOnly? dateCreation, IDictionary<string, string>? dictionary, int? templateId, int? userId)
    {
        if (string.IsNullOrEmpty(name) || dictionary is null || templateId <= 0 || userId <= 0)
        {
            throw new ValidationException("Некорректные данные");
        }
        return new DocumentModel(name, dateCreation, dictionary, templateId, userId);
    }
}

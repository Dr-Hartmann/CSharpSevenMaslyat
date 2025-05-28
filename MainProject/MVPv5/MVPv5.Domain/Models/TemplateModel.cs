using MVPv5.Domain.Entities;

namespace MVPv5.Domain.Models;

public class TemplateModel
{
    private TemplateModel(TemplateEntity entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        Type = entity.Type;
        DateCreation = entity.DateCreation;
        Content = entity.Content;
        ContentType = entity.ContentType;
        Tags = entity.Tags;
    }
    private TemplateModel(string name, string? type, DateOnly? dateCreation, byte[] content, 
        string contentType, IEnumerable<string>? tags)
    {
        Name = name;
        Type = type;
        DateCreation = dateCreation ?? DateOnly.FromDateTime(DateTime.Now);
        Content = content;
        ContentType = contentType;
        Tags = tags;
    }

    public int Id { get; }
    public string Name { get; }
    public string? Type { get; }
    public DateOnly DateCreation { get; }
    public byte[] Content { get; }
    public string ContentType { get; }
    public IEnumerable<string>? Tags { get; set; }

    public static (TemplateModel Template, string Error) Create(TemplateEntity? entity)
    {
        if(entity is null)
        {
            throw new Exception("Передана пустая сущность");
        }
        var error = string.Empty;
        // TODO
        return (new TemplateModel(entity), error);
    }
    public static TemplateModel Create(string name, string? type, DateOnly? dateCreation,
        byte[] content, string contentType, IEnumerable<string>? tags)
    {
        // TODO
        return new TemplateModel(name, type, dateCreation, content, contentType, tags);
    }
}

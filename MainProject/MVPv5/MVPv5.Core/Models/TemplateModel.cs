namespace MVPv5.Core.Models;

public class TemplateModel
{
    private TemplateModel(int id, string name, string? type, DateOnly dateCreation, 
        byte[] content, string contentType, IEnumerable<string> tags)
    {
        Id = id;
        Name = name;
        Type = type;
        DateCreation = dateCreation;
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
    public IEnumerable<string> Tags { get; set; }

    public static (TemplateModel, string) Create(int id, string name, string? type, DateOnly dateCreation,
        byte[] content, string contentType, IEnumerable<string> tags)
    {
        var error = string.Empty;

        // TODO - добавить проверку

        return (new TemplateModel(id, name, type, dateCreation, content, contentType, tags), error);
    }
}

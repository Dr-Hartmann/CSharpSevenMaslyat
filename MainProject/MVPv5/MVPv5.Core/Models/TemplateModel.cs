namespace MVPv5.Core.Models;

public class TemplateModel
{
    private TemplateModel(int id, string name, string type, DateOnly dateCreation, byte[] content)
    {
        Id = id;
        Name = name;
        Type = type;
        DateCreation = dateCreation;
        Content = content;
    }

    public int Id { get; }
    public string Name { get; }
    public string Type { get; }
    public DateOnly DateCreation { get; }
    public byte[] Content { get; }

    public static (TemplateModel, string) Create(int id, string name, string type, DateOnly dateCreation, byte[] content)
    {
        var error = string.Empty;
        return (new TemplateModel(id, name, type, dateCreation, content), error);
    }
}

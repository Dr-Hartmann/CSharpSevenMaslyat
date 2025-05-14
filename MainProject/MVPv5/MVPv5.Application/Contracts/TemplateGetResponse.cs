namespace MVPv5.Application.Contracts;

public class TemplateGetResponse
{
    public int Id { get; set;  }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public DateOnly DateCreation { get; set; }
    public byte[]? Content { get; set; }

    public string ContentType { get; set; } = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
} 
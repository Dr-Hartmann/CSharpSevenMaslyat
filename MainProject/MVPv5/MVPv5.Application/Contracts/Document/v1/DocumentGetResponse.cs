namespace MVPv5.Application.Contracts.Document.v1;

public class DocumentGetResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly DateCreation { get; set; }
    //public JsonDocument? MetadataJson { get; set; }
    public int TemplateId { get; set; }
    public int UserId { get; set; }
} 
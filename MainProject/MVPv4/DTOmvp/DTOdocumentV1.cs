namespace DTOmvp;

public class DTOdocumentV1
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public byte[]? File { get; set; }
    public DateOnly? Year { get; set; }
    public string? Title { get; set; }
    public string? Topic { get; set; }
    public string? Annotation { get; set; }
}

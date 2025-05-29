namespace MVPv5.Application.Contracts.Template.v1;

public record TemplateReadResponse(
    int Id,
    string? Name,
    string? Type,
    DateOnly? DateCreation,
    byte[]? Content,
    string? ContentType,
    IEnumerable<string>? Tags);

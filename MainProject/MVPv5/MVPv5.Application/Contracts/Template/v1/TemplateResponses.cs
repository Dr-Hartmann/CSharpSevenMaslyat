namespace MVPv5.Application.Contracts.Template.v1;

public record TemplateReadResponse(
    int id,
    string? name,
    string? type,
    DateOnly? dateCreation,
    byte[]? content,
    string? contentType,
    IEnumerable<string>? tags);

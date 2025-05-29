namespace MVPv5.Application.Contracts.Document.v1;

public record DocumentReadResponse(
    int Id,
    string? Name,
    DateOnly? DateCreation,
    IDictionary<string, string>? Data,
    int TemplateId,
    int UserId);
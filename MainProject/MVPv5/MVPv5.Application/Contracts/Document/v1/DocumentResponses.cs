namespace MVPv5.Application.Contracts.Document.v1;

public record DocumentBuildResponse(
    string? name,
    byte[]? content);

public record DocumentReadResponse(
    int id,
    string? name,
    DateOnly? dateCreation,
    string? metadataJson,
    int templateId,
    int userId);
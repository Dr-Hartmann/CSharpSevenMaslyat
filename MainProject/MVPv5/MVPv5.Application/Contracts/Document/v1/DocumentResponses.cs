namespace MVPv5.Application.Contracts.Document.v1;

public record DocumentBuildResponse(
    string? name,
    byte[]? content);
using System.Text.Json;
using MVPv5.Core.Models;

namespace MVPv5.Core.Abstractions.v1;

public interface IDocumentRepository
{
    Task AddAsync(string name, DateOnly dateCreation, JsonDocument? metadataJson,
        int templateId, int userId, CancellationToken token);

    Task<(DocumentModel Document, string Error)> GetByIdAsync(int id, CancellationToken token);

    Task<IEnumerable<(DocumentModel Document, string Error)>> GetAllAsync(CancellationToken token);

    Task UpdateNameAsync(int id, string name, CancellationToken token);

    Task UpdateMetaDataAsync(int id, JsonDocument metadataJson, CancellationToken token);

    Task UpdateAsync(int id, string name, DateOnly dateCreation, JsonDocument? metadataJson,
        int templateId, int userId, CancellationToken token);

    Task DeleteById(int id, CancellationToken token);
}

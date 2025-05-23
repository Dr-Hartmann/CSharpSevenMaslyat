using System.Text.Json;
using MVPv5.Core.Models;

namespace MVPv5.Core.Abstractions.v1;

public interface IDocumentService
{
    Task CreateAsync(string name, DateOnly dateCreation, JsonDocument metadataJson,
        int templateId, int userId, CancellationToken token);

    Task<DocumentModel> GetByIdAsync(int id, CancellationToken token);
    Task<IEnumerable<DocumentModel>> GetAllAsync(CancellationToken token);
    Task UpdateMetaDataById(int id, JsonDocument metadataJson, CancellationToken token);

}

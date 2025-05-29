using MVPv5.Domain.Models;

namespace MVPv5.Domain.Abstractions.v1;

public interface IDocumentService
{
    Task CreateAsync(DocumentModel model, CancellationToken token);
    Task<DocumentModel> GetByIdAsync(int id, CancellationToken token);
    Task<IEnumerable<DocumentModel>> GetAllAsync(CancellationToken token);
    Task UpdateNameAsync(int id, string? name, CancellationToken token);
    Task UpdateMetaDataById(int id, IDictionary<string, string>? metadataJson, CancellationToken token);
    Task UpdateAsync(int id, DocumentModel model, CancellationToken token);
    Task DeleteByIdAsync(int id, CancellationToken token);
}
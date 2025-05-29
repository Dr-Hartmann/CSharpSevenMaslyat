using MVPv5.Domain.Models;

namespace MVPv5.Domain.Abstractions.v1;

public interface IDocumentRepository
{
    Task AddAsync(DocumentModel model, CancellationToken token);
    Task<(DocumentModel Document, string Error)> GetByIdAsync(int id, CancellationToken token);
    Task<IEnumerable<(DocumentModel Document, string Error)>> GetAllAsync(CancellationToken token);
    Task UpdateNameAsync(int id, string name, CancellationToken token);
    Task UpdateMetaDataAsync(int id, IDictionary<string, string> metadataJson, CancellationToken token);
    Task UpdateAsync(int id, DocumentModel model, CancellationToken token);
    Task DeleteById(int id, CancellationToken token);
}

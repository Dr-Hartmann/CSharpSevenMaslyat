using MVPv5.Domain.Abstractions.v1;
using MVPv5.Domain.Models;

namespace MVPv5.Domain.Services.v1;

public class DocumentService(IDocumentRepository repository) : IDocumentService
{
    public async Task CreateAsync(DocumentModel model, CancellationToken token)
    {
        await repository.AddAsync(model, token);
    }

    public async Task<DocumentModel> GetByIdAsync(int id, CancellationToken token)
    {
        var response = await repository.GetByIdAsync(id, token);

        if (response.Error != string.Empty)
        {
            throw new KeyNotFoundException($"Ошибка: {response.Error}");
        }

        return response.Document;
    }

    public async Task<IEnumerable<DocumentModel>> GetAllAsync(CancellationToken token)
    {
        var response = await repository.GetAllAsync(token);
        var errors = response.Where(resp => !string.IsNullOrWhiteSpace(resp.Error)).ToList();
        if (errors.Any())
        {
            var errMessages = string.Join(" | ", errors.Select(e => e.Error));
            throw new ApplicationException($"Обнаружены ошибки при получении документов: {errMessages}");
        }
        return response.Select(l => l.Document);
    }

    public async Task UpdateMetaDataById(int id, IDictionary<string, string>? metadataJson, CancellationToken token)
    {
        if (metadataJson is null)
        {
            throw new Exception("Данные тегов пусты");
        }
        await repository.UpdateMetaDataAsync(id, metadataJson, token);
    }

    public async Task UpdateNameAsync(int id, string? name, CancellationToken token)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new Exception("Имя пусто");
        }
        await repository.UpdateNameAsync(id, name, token);
    }

    public async Task UpdateAsync(int id, DocumentModel model, CancellationToken token)
    {
        await repository.UpdateAsync(id, model, token);
    }

    public async Task DeleteByIdAsync(int id, CancellationToken token)
    {
        await repository.DeleteById(id, token);
    }
}

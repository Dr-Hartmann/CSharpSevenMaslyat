using System.Text.Json;
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
        // TODO - логика обработки ошибок при создании модели
        return response.Select(l => l.Document);
    }

    public async Task UpdateMetaDataById(int id, JsonDocument metadataJson, CancellationToken token)
    {
        await repository.UpdateMetaDataAsync(id, metadataJson, token);
    }

    public async Task UpdateNameAsync(int id, string name, CancellationToken token)
    {
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

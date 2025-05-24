using System.Text.Json;
using MVPv5.Core.Abstractions.v1;
using MVPv5.Core.Models;

namespace MVPv5.Application.Services.v1;

public class DocumentService(IDocumentRepository repository) : IDocumentService
{
    public async Task CreateAsync(string name, DateOnly dateCreation, JsonDocument metadataJson,
        int templateId, int userId, CancellationToken token)
    {
        await repository.AddAsync(name, dateCreation, metadataJson, templateId, userId, token);
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
        return (await repository.GetAllAsync(token)).Select(l => l.Document);
    }

    public async Task UpdateMetaDataById(int id, JsonDocument metadataJson, CancellationToken token)
    {
        await repository.UpdateMetaDataAsync(id, metadataJson, token);
    }

    public async Task UpdateNameAsync(int id, string name, CancellationToken token)
    {
        await repository.UpdateNameAsync(id, name, token);
    }

    public async Task UpdateAsync(int id, string name, DateOnly dateCreation, JsonDocument metadataJson,
        int templateId, int userId, CancellationToken token)
    {
        await repository.UpdateAsync(id, name, dateCreation, metadataJson, templateId, userId, token);
    }

    public async Task DeleteByIdAsync(int id, CancellationToken token)
    {
        await repository.DeleteById(id, token);
    }
}

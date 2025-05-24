using MVPv5.Core.Abstractions.v1;
using MVPv5.Core.Models;
using MVPv5.Domain.Repositories;

namespace MVPv5.Application.Services.v1;

public class TemplateService(ITemplateRepository repository) : ITemplateService
{
    public async Task AddAsync(string name, string? type, DateOnly dateCreation,
        byte[] content, string contentType, IEnumerable<string> tags, CancellationToken token)
    {
        await repository.AddAsync(name, type, dateCreation, content, contentType, tags, token);
    }

    public async Task<TemplateModel> GetByIdAsync(int id, CancellationToken token)
    {
        var response = await repository.GetByIdAsync(id, token);

        if (!string.IsNullOrWhiteSpace(response.Error))
        {
            throw new KeyNotFoundException($"Шаблон не найден (ID = {id}): {response.Error}");
        }

        return response.Template;
    }
    public async Task<IEnumerable<TemplateModel>> GetAllAsync(CancellationToken token)
    {
        var response = await repository.GetAllAsync(token);

        var errors = response.Where(resp => !string.IsNullOrWhiteSpace(resp.Error)).ToList();
        if (errors.Count > 0)
        {
            var errMessages = string.Join(" | ", errors.Select(e => e.Error));
            throw new ApplicationException($"Обнаружены ошибки при получении шаблонов: {errMessages}");
        }

        return response.Select(resp => resp.Template);
    }
    public async Task PatchAsync(int id, string? name, string? type, byte[]? content, 
        string? contentType, IEnumerable<string>? tags, CancellationToken token)
    {
        await repository.PatchAsync(id, name, type, content, contentType, tags, token);
    }

    public async Task UpdateAsync(int id, string name, string? type, DateOnly dateCreation,
        byte[] content, string contentType, IEnumerable<string> tags, CancellationToken token)
    {
        await repository.UpdateAsync(id, name, type, dateCreation, content, contentType, tags, token);
    }
    public async Task DeleteByIdAsync(int id, CancellationToken token)
    {
        await repository.DeleteByIdAsync(id, token);
    }

}

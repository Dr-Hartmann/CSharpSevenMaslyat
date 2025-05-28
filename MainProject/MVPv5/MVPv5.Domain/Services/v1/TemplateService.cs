using MVPv5.Domain.Abstractions.v1;
using MVPv5.Domain.Models;
using MVPv5.Domain.Repositories;

namespace MVPv5.Domain.Services.v1;

public class TemplateService(ITemplateRepository repository) : ITemplateService
{
    public async Task AddAsync(TemplateModel model, CancellationToken token)
    {
        await repository.AddAsync(model, token);
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
        if (errors.Any())
        {
            var errMessages = string.Join(" | ", errors.Select(e => e.Error));
            throw new ApplicationException($"Обнаружены ошибки при получении шаблонов: {errMessages}");
        }
        return response.Select(resp => resp.Template);
    }

    public async Task UpdateNameAsync(int id, string? name, CancellationToken token)
    {
        if (name is null) return;
        await repository.UpdateNameAsync(id, name, token);
    }

    public async Task UpdateTypeAsync(int id, string? type, CancellationToken token)
    {
        if (type is null) return;
        await repository.UpdateTypeAsync(id, type, token);
    }

    public async Task UpdateContentAndContentTypeAsync(int id, byte[]? content, string? contentType, CancellationToken token)
    {
        if (content is null || contentType is null) return;
        await repository.UpdateContentAndContentTypeAsync(id, content, contentType, token);
    }

    public async Task UpdateTagsAsync(int id, IEnumerable<string>? tags, CancellationToken token)
    {
        if (tags is null) return;
        await repository.UpdateTagsAsync(id, tags, token);
    }

    //public async Task UpdateAsync(int id, TemplateModel model, CancellationToken token)
    //{
    //    await repository.UpdateAsync(id, model, token);
    //}
    public async Task DeleteByIdAsync(int id, CancellationToken token)
    {
        await repository.DeleteByIdAsync(id, token);
    }
}

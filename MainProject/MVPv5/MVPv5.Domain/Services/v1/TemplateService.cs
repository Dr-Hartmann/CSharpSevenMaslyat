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
    public async Task PatchAsync(int id, TemplateModel model, CancellationToken token)
    {
        await repository.PatchAsync(id, model, token);
    }

    public async Task UpdateAsync(int id, TemplateModel model, CancellationToken token)
    {
        // TODO - наплодить Update`ов, см. Repository, и распределить выбросы исключений
        await repository.UpdateAsync(id, model, token);
    }
    public async Task DeleteByIdAsync(int id, CancellationToken token)
    {
        await repository.DeleteByIdAsync(id, token);
    }
}

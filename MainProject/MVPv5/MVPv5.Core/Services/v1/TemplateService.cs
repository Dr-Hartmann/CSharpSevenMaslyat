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

        if (response.Error != string.Empty)
        {
            throw new ArgumentException($"Ошибка: {response.Error}");
        }

        return response.Template;
    }

    public async Task<IEnumerable<TemplateModel>> GetAllAsync(CancellationToken token)
    {
        var response = await repository.GetAllAsync(token);
        var errors = response.Select(r => r.Error != string.Empty);

        if (errors.Count() > 0)
        {
            System.Text.StringBuilder str = new();
            response.Select(r => r.Error).Select(item => str.Append($"| {item}"));
            throw new Exception($"Ошибка: {str.ToString()}");
        }

        return response.Select(l => l.Template);
    }
}

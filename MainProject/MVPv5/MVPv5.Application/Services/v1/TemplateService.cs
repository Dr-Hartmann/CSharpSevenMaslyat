using MVPv5.Core.Abstractions.v1;
using MVPv5.Core.Models;
using MVPv5.Domain.Repositories;

namespace MVPv5.Application.Services.v1;

public class TemplateService(ITemplateRepository repository) : ITemplateService
{
    public async Task<int> CreateAsync(string name, string type, DateOnly dateCreation, byte[] content, CancellationToken token)
    {
        return await repository.AddAsync(name, type, dateCreation, content, token);
    }

    public async Task<TemplateModel> GetByIdAsync(int id, CancellationToken token)
    {
        var response = await repository.GetByIdAsync(id, token);

        if (response.Error != string.Empty)
        {
            throw new KeyNotFoundException($"Ошибка: {response.Error}");
        }

        return response.Template;
    }
}

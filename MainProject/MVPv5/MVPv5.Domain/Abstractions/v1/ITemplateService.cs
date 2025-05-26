using MVPv5.Domain.Models;

namespace MVPv5.Domain.Abstractions.v1;

public interface ITemplateService
{
    Task AddAsync(TemplateModel model, CancellationToken token);
    Task<TemplateModel> GetByIdAsync(int id, CancellationToken token);
    Task<IEnumerable<TemplateModel>> GetAllAsync(CancellationToken token);
    Task PatchAsync(int id, TemplateModel model, CancellationToken token);
    Task UpdateAsync(int id, TemplateModel model, CancellationToken token);
    Task DeleteByIdAsync(int id, CancellationToken token);
}

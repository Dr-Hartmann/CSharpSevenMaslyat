using MVPv5.Domain.Models;

namespace MVPv5.Domain.Repositories;
public interface ITemplateRepository
{
    Task AddAsync(TemplateModel model, CancellationToken token);
    Task<(TemplateModel Template, string Error)> GetByIdAsync(int id, CancellationToken token);
    Task<IEnumerable<(TemplateModel Template, string Error)>> GetAllAsync(CancellationToken token);
    Task UpdateAsync(int id, TemplateModel model, CancellationToken token);
    Task UpdateNameAsync(int id, string name, CancellationToken token);
    Task UpdateIdAsync (int id, CancellationToken token);
    Task DeleteByIdAsync(int id, CancellationToken token);
}
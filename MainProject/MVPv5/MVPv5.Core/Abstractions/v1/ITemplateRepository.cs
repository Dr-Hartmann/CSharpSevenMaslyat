using MVPv5.Core.Models;

namespace MVPv5.Domain.Repositories;
public interface ITemplateRepository
{
    Task<int> AddAsync(string name, string type, DateOnly dateCreation, byte[] content, CancellationToken token);
    Task<(TemplateModel Template, string Error)> GetByIdAsync(int id, CancellationToken token);
}
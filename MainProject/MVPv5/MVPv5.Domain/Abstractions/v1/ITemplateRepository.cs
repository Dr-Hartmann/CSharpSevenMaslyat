using MVPv5.Domain.Models;

namespace MVPv5.Domain.Repositories;
public interface ITemplateRepository
{
    Task AddAsync(TemplateModel model, CancellationToken token);
    Task<(TemplateModel Template, string Error)> GetByIdAsync(int id, CancellationToken token);
    Task<IEnumerable<(TemplateModel Template, string Error)>> GetAllAsync(CancellationToken token);
    Task UpdateAsync(int id, TemplateModel model, CancellationToken token);
    Task UpdateNameAsync(int id, string name, CancellationToken token);
    Task UpdateContentAndContentTypeAsync(int id, byte[] content, string contentType, CancellationToken token);
    Task UpdateTagsAsync(int id, IEnumerable<string> tags, CancellationToken token);
    Task DeleteByIdAsync(int id, CancellationToken token);
}
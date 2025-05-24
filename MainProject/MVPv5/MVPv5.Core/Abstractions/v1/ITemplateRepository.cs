using MVPv5.Core.Models;

namespace MVPv5.Domain.Repositories;
public interface ITemplateRepository
{
    Task AddAsync(string name, string? type, DateOnly dateCreation, byte[] content,
        string contentType, IEnumerable<string> tags, CancellationToken token);
    Task<(TemplateModel Template, string Error)> GetByIdAsync(int id, CancellationToken token);
    Task<IEnumerable<(TemplateModel Template, string Error)>> GetAllAsync(CancellationToken token);
    Task PatchAsync(int id, string? name, string? type, byte[]? content, string? contentType, 
        IEnumerable<string>? tags, CancellationToken token);
    Task UpdateAsync(int id, string name, string? type, DateOnly dateCreation, byte[] content, 
        string contentType, IEnumerable<string> tags, CancellationToken token);
    Task DeleteByIdAsync(int id, CancellationToken token);
}
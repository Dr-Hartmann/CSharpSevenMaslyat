using MVPv5.Core.Models;

namespace MVPv5.Core.Abstractions.v1;

public interface ITemplateService
{
    Task AddAsync(string name, string? type, DateOnly dateCreation, 
        byte[] content, string contentType, IEnumerable<string> tags, CancellationToken token);
    Task<TemplateModel> GetByIdAsync(int id, CancellationToken token);
    Task<IEnumerable<TemplateModel>> GetAllAsync(CancellationToken token);
}

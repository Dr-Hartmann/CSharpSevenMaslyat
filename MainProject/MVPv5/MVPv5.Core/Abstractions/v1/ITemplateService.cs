using MVPv5.Core.Models;

namespace MVPv5.Core.Abstractions.v1;

public interface ITemplateService
{
    public Task<int> CreateAsync(string name, string type, DateOnly dateCreation, byte[] content, CancellationToken token);
    public Task<TemplateModel> GetByIdAsync(int id, CancellationToken token);
}

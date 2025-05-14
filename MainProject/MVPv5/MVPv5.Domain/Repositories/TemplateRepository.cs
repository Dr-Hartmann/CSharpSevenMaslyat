using Microsoft.EntityFrameworkCore;
using MVPv5.Core.Models;
using MVPv5.Domain.Data;
using MVPv5.Domain.Entities;

namespace MVPv5.Domain.Repositories;

public class TemplateRepository(MVPv5DbContext dbContext) : ITemplateRepository
{
    public async Task AddAsync(string name, string? type, DateOnly dateCreation, byte[] content,
        string contentType, IEnumerable<string> tags, CancellationToken token)
    {
        if (dbContext!.Templates.Where(u => u.Name == name).Count() > 0)
        {
            throw new Exception("Такой шаблон уже существует");
        }

        await dbContext!.Templates.AddAsync(new TemplateEntity
        {
            Name = name,
            Type = type,
            DateCreation = dateCreation,
            Content = content,
            ContentType = contentType,
            Tags = tags.ToArray()
        }, token);

        await dbContext.SaveChangesAsync(token);
    }

    public async Task<(TemplateModel Template, string Error)> GetByIdAsync(int id, CancellationToken token)
    {
        return GetTemplate(await dbContext.Templates
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id, token));
    }

    public async Task<IEnumerable<(TemplateModel Template, string Error)>> GetAllAsync(CancellationToken token)
    {
        return GetListOfTemplates(await dbContext.Templates
            .AsNoTracking()
            .ToListAsync(token));
    }

    // TODO - Delete    Update

    private (TemplateModel Template, string Error) GetTemplate(TemplateEntity? response)
    {
        if (response == null) throw new Exception("Пустая сущность в ответе");
        return TemplateModel.Create(response.Id, response.Name, response.Type, response.DateCreation,
            response.Content, response.ContentType, response.Tags);
    }

    private IEnumerable<(TemplateModel Template, string Error)> GetListOfTemplates(IEnumerable<TemplateEntity>? response)
    {
        if (response == null) throw new Exception("Пустой лист в ответе");
        return response.Select(GetTemplate);
    }
}

using Microsoft.EntityFrameworkCore;
using MVPv5.Core.Models;
using MVPv5.Domain.Data;
using MVPv5.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MVPv5.Domain.Repositories;

public class TemplateRepository(MVPv5DbContext dbContext) : ITemplateRepository
{
    public async Task AddAsync(string name, string? type, DateOnly dateCreation, byte[] content,
        string contentType, IEnumerable<string> tags, CancellationToken token)
    {
        if (await dbContext.Templates.AnyAsync(u => u.Name == name, token))
        {
            throw new ValidationException("Такой шаблон уже существует");
        }

        var tmp = new TemplateEntity
        {
            Name = name,
            Type = type,
            DateCreation = dateCreation,
            Content = content,
            ContentType = contentType,
            Tags = tags.ToArray()
        };

        await dbContext!.Templates.AddAsync(tmp, token);

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

    public async Task PatchAsync(int id, string? name, string? type, byte[]? content, string? contentType, 
        IEnumerable<string>? tags, CancellationToken token)
    {
        var template = await dbContext.Templates.FirstOrDefaultAsync(t => t.Id == id, token);

        if (template == null)
            throw new KeyNotFoundException($"Шаблон не найден (ID = {id})");

        if (!string.IsNullOrWhiteSpace(name))
            template.Name = name;

        if (type != null)
            template.Type = type;

        if (content != null)
            template.Content = content;

        if (!string.IsNullOrWhiteSpace(contentType))
            template.ContentType = contentType;

        if (tags != null)
            template.Tags = tags.ToArray();

        await dbContext.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(int id, string name, string? type, DateOnly dateCreation, 
        byte[] content, string contentType, IEnumerable<string> tags, CancellationToken token)
    {
        var template = await dbContext.Templates.FirstOrDefaultAsync(t => t.Id == id, token);

        if (template == null)
            throw new KeyNotFoundException($"Шаблон не найден (ID = {id})");

        if (await dbContext.Templates.AnyAsync(t => t.Id != id && t.Name == name, token))
        {
            throw new ValidationException("Шаблон с таким именем уже существует.");
        }

        template.Name = name;
        template.Type = type;
        template.DateCreation = dateCreation;
        template.Content = content;
        template.ContentType = contentType;
        template.Tags = tags.ToArray();

        await dbContext.SaveChangesAsync(token);
    }

    public async Task DeleteByIdAsync(int id, CancellationToken token)
    {
        var count = await dbContext.Templates
            .Where(template => template.Id == id)
            .ExecuteDeleteAsync(token);

        if (count != 1)
        {
            throw new DbUpdateException($"Удалено {count} шаблонов вместо 1");
        }
    }

    private (TemplateModel Template, string Error) GetTemplate(TemplateEntity? response)
    {
        if (response == null) throw new KeyNotFoundException("Пустая сущность в ответе");
        return TemplateModel.Create(response.Id, response.Name, response.Type, response.DateCreation,
            response.Content, response.ContentType, response.Tags);
    }

    private IEnumerable<(TemplateModel Template, string Error)> GetListOfTemplates(IEnumerable<TemplateEntity>? response)
    {
        if (response == null) throw new KeyNotFoundException("Пустой лист в ответе");
        return response.Select(GetTemplate);
    }
}

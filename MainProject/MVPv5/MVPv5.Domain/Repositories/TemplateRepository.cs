using Microsoft.EntityFrameworkCore;
using MVPv5.Domain.Data;
using MVPv5.Domain.Entities;
using MVPv5.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace MVPv5.Domain.Repositories;

public class TemplateRepository(MVPv5DbContext dbContext) : ITemplateRepository
{
    public async Task AddAsync(TemplateModel model, CancellationToken token)
    {
        if (await dbContext.Templates.AnyAsync(u => u.Name == model.Name, token))
        {
            throw new ValidationException("Такой шаблон уже существует");
        }

        await dbContext!.Templates.AddAsync(new TemplateEntity
        {
            Name = model.Name,
            Type = model.Type,
            DateCreation = model.DateCreation!,
            Content = model.Content,
            ContentType = model.ContentType,
            Tags = [.. model.Tags]
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

    // TODO - переписать Update и Patch, разделив ответственность ПОЛНОЙ замены и частичного обновления
    // проще говоря - наплодите Update...Async
    // переместите все проверки на корректность входных данных в Service, Repository только работает с БД
    // PUT меняет объект целиком.
    // PATCH изменяет отдельные поля ресурса.

    // TODODO - у вас хорошо написано в DocumentRepository, повторить
    public async Task PatchAsync(int id, TemplateModel model, CancellationToken token)
    {
        var template = await dbContext.Templates.FirstOrDefaultAsync(t => t.Id == id, token);
        if (template == null)
        {
            throw new KeyNotFoundException($"Шаблон не найден (ID = {id})");
        }
        template.Name = model.Name;
        template.Type = model.Type;
        template.Content = model.Content;
        template.ContentType = model.ContentType;
        template.Tags = [.. model.Tags];
        await dbContext.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(int id, TemplateModel model, CancellationToken token)
    {
        var template = await dbContext.Templates.FirstOrDefaultAsync(t => t.Id == id, token);
        if (template == null)
        {
            throw new KeyNotFoundException($"Шаблон не найден (ID = {id})");
        }
        if (await dbContext.Templates.AnyAsync(t => t.Id != id && t.Name == model.Name, token))
        {
            throw new ValidationException("Шаблон с таким именем уже существует.");
        }
        template.Name = model.Name;
        template.Type = model.Type;
        template.Content = model.Content;
        template.ContentType = model.ContentType;
        template.Tags = [.. model.Tags];
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
        return TemplateModel.Create(response);
    }

    private IEnumerable<(TemplateModel Template, string Error)> GetListOfTemplates(IEnumerable<TemplateEntity>? response)
    {
        if (response == null) throw new Exception("Пустой лист в ответе");
        return response.Select(GetTemplate);
    }
}

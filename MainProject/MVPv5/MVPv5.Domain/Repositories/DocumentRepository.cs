using Microsoft.EntityFrameworkCore;
using MVPv5.Core.Models;
using MVPv5.Domain.Data;
using MVPv5.Domain.Entities;

namespace MVPv5.Domain.Repositories;

public class DocumentRepository(MVPv5DbContext dbContext)
{
    //public async Task AddAsync(string name, string? type, DateOnly dateCreation, byte[] content,
    //    string contentType, IEnumerable<string> tags, CancellationToken token)
    //{
    //    if (dbContext!.Templates.Where(u => u.Name == name).Count() > 0)
    //    {
    //        throw new Exception("Такой шаблон уже существует");
    //    }

    //    await dbContext!.Templates.AddAsync(new TemplateEntity
    //    {
    //        Name = name,
    //        Type = type,
    //        DateCreation = dateCreation,
    //        Content = content,
    //        ContentType = contentType,
    //        Tags = tags.ToArray()
    //    }, token);

    //    await dbContext.SaveChangesAsync(token);
    //}

    //public async Task<(TemplateModel Template, string Error)> GetByIdAsync(int id, CancellationToken token)
    //{
    //    return GetTemplate(await dbContext.Templates
    //        .AsNoTracking()
    //        .FirstOrDefaultAsync(t => t.Id == id, token));
    //}

    //private (TemplateModel User, string Error) GetTemplate(TemplateEntity? response)
    //{
    //    if (response == null) throw new Exception("Нет такого шаблона");
    //    return TemplateModel.Create(response.Id, response.Name, response.Type, response.DateCreation,
    //        response.Content, response.ContentType, response.Tags);
    //}
}

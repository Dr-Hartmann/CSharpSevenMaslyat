using Microsoft.EntityFrameworkCore;
using MVPv5.Core.Models;
using MVPv5.Domain.Data;
using MVPv5.Domain.Entities;

namespace MVPv5.Domain.Repositories;

public class TemplateRepository(MVPv5DbContext dbContext) : ITemplateRepository
{
    public async Task<int> AddAsync(string name, string type, DateOnly dateCreation, byte[] content, CancellationToken token)
    {
        if (dbContext!.Templates.Where(u => u.Name == name).Count() > 0)
        {
            return -1;
        }

        await dbContext!.Templates.AddAsync(new TemplateEntity
        {
            Name = name,
            Type = type,
            DateCreation = dateCreation,
            Content = content
        }, token);
        await dbContext.SaveChangesAsync(token);

        return dbContext!.Templates.FirstOrDefaultAsync(u => u.Name == name, token).Id;
    }

    public async Task<(TemplateModel Template, string Error)> GetByIdAsync(int id, CancellationToken token)
    {
        return GetTemplate(await dbContext.Templates
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id, token));
    }

    private (TemplateModel User, string Error) GetTemplate(TemplateEntity? response)
    {
        if (response == null) throw new Exception("Пустой пользователь в ответе");
        return TemplateModel.Create(response.Id, response.Name, response.Type, response.DateCreation, response.Content);
    }
}

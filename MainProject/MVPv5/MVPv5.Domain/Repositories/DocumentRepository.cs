using Microsoft.EntityFrameworkCore;
using MVPv5.Core.Models;
using MVPv5.Domain.Data;
using MVPv5.Domain.Entities;
using System.Text.Json;

namespace MVPv5.Domain.Repositories;

public class DocumentRepository(MVPv5DbContext dbContext)
{
    public async Task AddAsync(string name, DateOnly dateCreation, JsonDocument? metadataJson,
        int templateId, int userId, CancellationToken token)
    {
        if (dbContext!.Documents.Where(u => u.Name == name).Count() > 0)
        {
            throw new Exception("Такой документ уже существует");
        }

        await dbContext!.Documents.AddAsync(new DocumentEntity
        {
            Name = name,
            DateCreation = dateCreation,
            MetadataJson = metadataJson,
            TemplateId = templateId,
            UserId = userId,
        }, token);

        await dbContext.SaveChangesAsync(token);
    }

    //какие еще должны быть????

    public async Task<(DocumentModel Template, string Error)> GetByIdAsync(int id, CancellationToken token)
    {
        return GetDocument(await dbContext.Documents
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id, token));
    }

    private (DocumentModel Document, string Error) GetDocument(DocumentEntity? response)
    {
        if (response == null) throw new Exception("Нет такого шаблона");
        return DocumentModel.Create(response.Id, response.Name, response.TemplateId, response.UserId, 
            response.DateCreation, response.MetadataJson);
    }
    
}

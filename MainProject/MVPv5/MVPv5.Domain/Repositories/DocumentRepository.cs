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

    //Дополнен репозиторий в соответствии с принципом CRUD (Create, Read, Update, Delete)
    //-------------Update------------------
    public async Task UpdateNameAsync(int id, string name, CancellationToken token)
    {
        await dbContext.Documents
            .Where(document => document.Id == id)
            .ExecuteUpdateAsync(document => document
                .SetProperty(u => u.Name, name),
                token);
        await dbContext.SaveChangesAsync(token);
    }

    public async Task UpdateMetaDataAsync(int id, JsonDocument? metadataJson, CancellationToken token)
    {
        await dbContext.Documents
            .Where(document => document.Id == id)
            .ExecuteUpdateAsync(document => document
                .SetProperty(u => u.MetadataJson, metadataJson),
                token);

        await dbContext.SaveChangesAsync(token);
    }

    //-------------Delete------------------
    public async Task DeleteById(int id, CancellationToken token)
    {
        var count = await dbContext.Documents
            .Where(document => document.Id == id)
            .ExecuteDeleteAsync(token);

        await dbContext.SaveChangesAsync(token);

        if (count != 1)
        {
            throw new Exception($"Удалено {count} документов вместо 1");
        }
    }

    public async Task<(DocumentModel Document, string Error)> GetByIdAsync(int id, CancellationToken token)
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

    private IEnumerable<(DocumentModel Document, string Error)> GetListOfDocuments(IEnumerable<DocumentEntity>? response)
    {
        if (response == null) throw new Exception("Пустой лист в ответе");
        return response.Select(GetDocument);
    }
}

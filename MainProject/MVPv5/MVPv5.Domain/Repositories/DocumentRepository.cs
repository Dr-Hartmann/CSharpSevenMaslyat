using Microsoft.EntityFrameworkCore;
using MVPv5.Core.Models;
using MVPv5.Domain.Data;
using MVPv5.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MVPv5.Domain.Repositories;

public class DocumentRepository(MVPv5DbContext dbContext)
{
    public async Task AddAsync(string name, DateOnly dateCreation, JsonDocument? metadataJson,
        int templateId, int userId, CancellationToken token)
    {
        if (await dbContext.Documents.AnyAsync(u => u.Name == name, token))
        {
            throw new ValidationException("Такой документ уже существует");
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
    public async Task<(DocumentModel Document, string Error)> GetByIdAsync(int id, CancellationToken token)
    {
        return GetDocument(await dbContext.Documents
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id, token));
    }

    public async Task<IEnumerable<(DocumentModel Document, string Error)>> GetAllAsync(CancellationToken token)
    {
        return GetListOfDocuments(await dbContext.Documents
            .AsNoTracking()
            .ToListAsync(token));
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

    public async Task UpdateAsync(int id, string name, DateOnly dateCreation, JsonDocument? metadataJson,
        int templateId, int userId, CancellationToken token)
    {
        var document = await dbContext.Documents.FirstOrDefaultAsync(d => d.Id == id, token);

        if (document == null)
        {
            throw new Exception("Документ не найден");
        }

        if (document.Name != name &&
            await dbContext.Documents.AnyAsync(d => d.Name == name, token))
        {
            throw new Exception("Документ с таким именем уже существует");
        }

        document.Name = name;
        document.DateCreation = dateCreation;
        document.MetadataJson = metadataJson;
        document.TemplateId = templateId;
        document.UserId = userId;

        await dbContext.SaveChangesAsync(token);
    }

    //-------------Delete------------------
    public async Task DeleteById(int id, CancellationToken token)
    {
        var count = await dbContext.Documents
            .Where(document => document.Id == id)
            .ExecuteDeleteAsync(token);

        if (count != 1)
        {
            throw new DbUpdateException($"Удалено {count} документов вместо 1");
        }
    }

    private (DocumentModel Document, string Error) GetDocument(DocumentEntity? response)
    {
        if (response == null) throw new KeyNotFoundException("Нет такого шаблона");
        return DocumentModel.Create(response.Id, response.Name, response.TemplateId, response.UserId, 
            response.DateCreation, response.MetadataJson);
    }

    private IEnumerable<(DocumentModel Document, string Error)> GetListOfDocuments(IEnumerable<DocumentEntity>? response)
    {
        if (response == null) throw new KeyNotFoundException("Пустой лист в ответе");
        return response.Select(GetDocument);
    }
}

using Microsoft.EntityFrameworkCore;
using MVPv5.Domain.Abstractions.v1;
using MVPv5.Domain.Data;
using MVPv5.Domain.Entities;
using MVPv5.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MVPv5.Domain.Repositories;

public class DocumentRepository(MVPv5DbContext dbContext) : IDocumentRepository
{
    public async Task AddAsync(DocumentModel model, CancellationToken token)
    {
        if (await dbContext.Documents.AnyAsync(u => u.Name == model.Name, token))
        {
            throw new ValidationException("Такой документ уже существует");
        }

        await dbContext!.Documents.AddAsync(new DocumentEntity
        {
            Name = model.Name,
            DateCreation = model.DateCreation!,
            MetadataJson = JsonDocument.Parse(JsonSerializer.Serialize(model.Dictionary)),
            TemplateId = model.TemplateId,
            UserId = model.UserId,
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

    public async Task UpdateNameAsync(int id, string name, CancellationToken token)
    {
        await dbContext.Documents
            .Where(document => document.Id == id)
            .ExecuteUpdateAsync(document => document
                .SetProperty(u => u.Name, name),
                token);
        await dbContext.SaveChangesAsync(token);
    }

    public async Task UpdateMetaDataAsync(int id, IDictionary<string, string> metadataJson, CancellationToken token)
    {
        var a = JsonDocument.Parse(JsonSerializer.Serialize(metadataJson));
        await dbContext.Documents
            .Where(document => document.Id == id)
            .ExecuteUpdateAsync(document => document
                .SetProperty(u => u.MetadataJson, a),
                token);
        await dbContext.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(int id, DocumentModel model, CancellationToken token)
    {
        var document = await dbContext.Documents.FirstOrDefaultAsync(d => d.Id == model.Id, token);
        if (document == null)
        {
            throw new Exception("Документ не найден");
        }
        if (document.Name != model.Name && await dbContext.Documents.AnyAsync(d => d.Name == model.Name, token))
        {
            throw new Exception("Документ с таким именем уже существует");
        }
        document.Name = model.Name;
        document.MetadataJson = JsonDocument.Parse(JsonSerializer.Serialize(model.Dictionary));
        document.TemplateId = model.TemplateId;
        document.UserId = model.UserId;
        await dbContext.SaveChangesAsync(token);
    }

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
        return DocumentModel.Create(response);
    }

    private IEnumerable<(DocumentModel Document, string Error)> GetListOfDocuments(IEnumerable<DocumentEntity>? response)
    {
        if (response == null) throw new Exception("Пустой лист в ответе");
        return response.Select(GetDocument);
    }
}

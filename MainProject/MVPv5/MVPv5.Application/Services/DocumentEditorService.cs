using MVPv5.Core.Models;
using MVPv5.Domain.Data;
using MVPv5.Domain.Entities;

public static class DocumentMapper
{
    public static DocumentEntity ToEntity(DocumentModel model)
    {
        return new DocumentEntity
        {
            Id = 0, // или model.Id
            UserId = model.UserId,
            MetadataJson = JsonHelper.Serialize(model.Metadata)
        };
    }

    public static DocumentModel ToModel(DocumentEntity entity)
    {
        return new DocumentModel
        {
            UserId = entity.UserId,
            Metadata = string.IsNullOrEmpty(entity.MetadataJson)
                ? new Dictionary<string, object>()
                : JsonHelper.Deserialize<Dictionary<string, object>>(entity.MetadataJson)
        };
    }
}

public class DocumentEditorService
{
    private readonly MVPv5DbContext dbContext;
    public DocumentEditorService(MVPv5DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void SaveDocument(DocumentModel model)
    {
        var entity = DocumentMapper.ToEntity(model);
        dbContext.TableDocuments.Add(entity);
        dbContext.SaveChanges();
    }

    public DocumentModel LoadDocument(int id)
    {
        var entity = dbContext.TableDocuments.Find(id);
        return DocumentMapper.ToModel(entity);
    }
}
//using DTOmvp;
//using Microsoft.EntityFrameworkCore;
//using MVPv4.Data;
//using MVPv4.Models;

//namespace MVPv4.Services;

//public class DocumentEditorService(IServiceScopeFactory scopeFactory, MVPv4Context dbContext) : IDocumentEditorService
//{
//    public async Task<DTOdocumentV1> GetAsync(int? id, CancellationToken token)
//    {
//        var document = await dbContext!.DocumentV1.FirstOrDefaultAsync(m => m.Id == id, token);
//        return new DTOdocumentV1
//        {
//            Id = document!.Id,
//            Title = document!.Title,
//            Year = document!.Year,
//            Topic = document!.Topic,
//            Annotation = document!.Annotation,
//        };
//    }

//    public async Task<IEnumerable<DTOdocumentV1>> GetAllAsync(CancellationToken cancellationToken)
//    {
//        var documents = await dbContext!.DocumentV1.Select(p => new DTOdocumentV1
//        {
//            Id = p!.Id,
//            Title = p!.Title,
//            Year = p!.Year,
//            Topic = p!.Topic,
//            Annotation = p!.Annotation
//        }).ToListAsync(cancellationToken);
//        return documents;
//    }

//    public async Task AddAsync(DTOdocumentV1 product, CancellationToken cancellationToken)
//    {
//        using var scope = scopeFactory.CreateScope();
//        var dbContext = scope.ServiceProvider.GetService<MVPv4Context>();
//        await dbContext!.DocumentV1.AddAsync(new DocumentV1
//        {
//            Name = product.Name,
//            File = product.File,
//            Title = product.Title,
//            Topic = product.Topic,
//            Year = product.Year,
//            Annotation = product.Annotation
//        });
//        await dbContext.SaveChangesAsync(cancellationToken);
//    }

//    public async Task UpdateAsync(DTOdocumentV1 product, CancellationToken cancellationToken)
//    {
//        using var scope = scopeFactory.CreateScope();
//        var dbContext = scope.ServiceProvider.GetRequiredService<MVPv4Context>();
//        var entity = await dbContext.DocumentV1.FindAsync(product.Id);
//        if (entity == null) throw new KeyNotFoundException();

//        entity.Name = product.Name;
//        entity.File = product.File;
//        entity.Title = product.Title;
//        entity.Topic = product.Topic;
//        entity.Year = product.Year;
//        entity.Annotation = product.Annotation;

//        await dbContext.SaveChangesAsync(cancellationToken);
//    }

//    public async Task DeleteAsync(int? id, CancellationToken cancellationToken)
//    {
//        using var scope = scopeFactory.CreateScope();
//        var dbContext = scope.ServiceProvider.GetRequiredService<MVPv4Context>();
//        var entity = await dbContext.DocumentV1.FindAsync(id);
//        if (entity == null) throw new KeyNotFoundException();

//        dbContext.DocumentV1.Remove(entity);
//        await dbContext.SaveChangesAsync(cancellationToken);
//    }

//    /* TODO
//     * CancellationToken - что это, как работает и как подключать.
//     * Дописать/создать новые сервисы, контроллеры,
//     * обеспечить возможность связи с разными контекстами (один сервис - один контекст),
//     * связать фабрику с контекстами и сервисами
//     */
//}

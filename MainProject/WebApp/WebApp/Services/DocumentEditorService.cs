using DTOmvp;
using Microsoft.EntityFrameworkCore;
using MVPv4.Data;
using MVPv4.Models;

namespace MVPv4.Services;

public class DocumentEditorService(IServiceScopeFactory scopeFactory, MVPv4Context dbContext) : IDocumentEditorService
{
    public async Task<DTOdocumentV1> GetAsync(int? id, CancellationToken token)
    {
        var document = await dbContext!.DocumentV1.FirstOrDefaultAsync(m => m.Id == id, token);
        return new DTOdocumentV1
        {
            Id = document!.Id,
            Title = document!.Title,
            Year = document!.Year,
            Topic = document!.Topic,
            Annotation = document!.Annotation,
        };
    }

    //TODO аналогично дополнить

    public async Task<IEnumerable<DTOdocumentV1>> GetAllAsync(CancellationToken cancellationToken)
    {
        var documents = await dbContext!.DocumentV1.Select(p => new DTOdocumentV1
        {
            Id = p!.Id,
            Title = p!.Title,
            Year = p!.Year,
            Topic = p!.Topic,
            Annotation = p!.Annotation
        }).ToListAsync(cancellationToken);
        return documents;
    }

    public async Task AddAsync(DTOdocumentV1 product)
    {
        using var scope = scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<MVPv4Context>();
        await dbContext!.DocumentV1.AddAsync(new DocumentV1
        {
            Name = product.Name,
            File = product.File,
            Title = product.Title,
            Topic = product.Topic,
            Year = product.Year,
            Annotation = product.Annotation
        });
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(DTOdocumentV1 product)
    {
        using var scope = scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MVPv4Context>();
        var entity = await dbContext.DocumentV1.FindAsync(product.Id);
        if (entity == null) throw new KeyNotFoundException();

        entity.Name = product.Name;
        entity.File = product.File;
        entity.Title = product.Title;
        entity.Topic = product.Topic;
        entity.Year = product.Year;
        entity.Annotation = product.Annotation;

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int? id)
    {
        using var scope = scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MVPv4Context>();
        var entity = await dbContext.DocumentV1.FindAsync(id);
        if (entity == null) throw new KeyNotFoundException();

        dbContext.DocumentV1.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    /* TODO
     * CancellationToken - что это, как работает и как подключать.
     * Дописать/создать новые сервисы, контроллеры,
     * обеспечить возможность связи с разными контекстами (один сервис - один контекст),
     * связать фабрику с контекстами и сервисами
     */
}

using DTOmvp;
using Microsoft.EntityFrameworkCore;
using MVPv4.Data;
using MVPv4.Models;

namespace MVPv4.Services;

public class DocumentEditorService(MVPv4Context dbContext) : IDocumentEditorService
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
        await dbContext!.DocumentV1.AddAsync(new DocumentV1
        {
            File = product.File,
            Title = product.Title,
            Topic = product.Topic,
            Year = product.Year,
            Annotation = product.Annotation
        });
        await dbContext.SaveChangesAsync();
    }

    //public Task DeleteAsync(int? id)
    //{
    //}

    //public Task<IEnumerable<DocumentV1>> GetAllAsync()
    //{
    //}

    //public Task<DocumentV1> GetByIdAsync(int? id)
    //{
    //}

    //public Task UpdateAsync(DocumentV1 product)
    //{
    //}

    /* TODO
     * Дописать/создать новые сервисы, контроллеры,
     * обеспечить возможность связи с разными контекстами (один сервис - один контекст),
     * связать фабрику с контекстами и сервисами
     */
}

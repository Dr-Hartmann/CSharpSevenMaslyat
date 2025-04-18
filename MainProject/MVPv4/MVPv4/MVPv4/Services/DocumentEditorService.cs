using DocumentFormat.OpenXml.Office2010.Excel;
using DTOmvp;
using Microsoft.EntityFrameworkCore;
using MVPv4.Data;

namespace MVPv4.Services;

public class DocumentEditorService(IServiceScopeFactory scopeFactory) : IDocumentEditorService
{
    public async Task<IEnumerable<DTOdocumentV1>> GetAll()
    {
        using var scope = scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<MVPv4Context>();
        var documents = await dbContext!.DocumentV1.Select(p => new DTOdocumentV1
        {
            Id = p!.Id,
            Title = p!.Title,
            Year = p!.Year,
            Topic = p!.Topic,
            Annotation = p!.Annotation
        }).ToListAsync();
        return documents;
    }

    public async Task<DTOdocumentV1> GetDocumentFromDatabase(int? id/*, CancellationToken token*/)
    {
        using var scope = scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<MVPv4Context>();
        var document = await dbContext!.DocumentV1.FirstOrDefaultAsync(m => m.Id == id);
        return new DTOdocumentV1
        {
            Id = document!.Id,
            Title = document!.Title,
            Year = document!.Year,
            Topic = document!.Topic,
            Annotation = document!.Annotation,
        };
    }

    //public Task AddAsync(DocumentV1 product)
    //{
    //} 

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
}

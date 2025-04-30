//using DTOmvp;
using MVPv5.Core.Models;
using MVPv5.Domain.Data;
using MVPv5.Domain.Entities;

public static class TemplateMapper
{
    public static TemplateEntity ToEntity(TemplateModel model)
    {
        return new TemplateEntity
        {
            Id = 0, // или model.Id
            MetadataJson = JsonHelper.Serialize(model.Metadata)
        };
    }

    public static TemplateModel ToModel(TemplateEntity entity)
    {
        return new TemplateModel
        {
            Metadata = string.IsNullOrEmpty(entity.MetadataJson)
                ? new Dictionary<string, object>()
                : JsonHelper.Deserialize<Dictionary<string, object>>(entity.MetadataJson)
        };
    }
}

public class GostDocumentEditorService
{
    private readonly MVPv5DbContext dbContext;
    public GostDocumentEditorService(MVPv5DbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    //public void SaveTemplate(TemplateModel model)
    //{
    //    var entity = TemplateMapper.ToEntity(model);
    //    dbContext.TableTemplates.Add(entity);
    //    dbContext.SaveChanges();
    //}

    //public TemplateModel LoadTemplate(int id)
    //{
    //    var entity = dbContext.TableTemplates.Find(id);
    //    return TemplateMapper.ToModel(entity);
    //}
}

//namespace WebApp.Services
//{
//    public class MockDocumentEditorService : IDocumentEditorService
//    {
//        private List<DTOdocumentV1> _mockDocs = new()
//    {
//        new() { Id = 1, Name = "Doc1", Title = "Title1", Year = new DateOnly(2022, 1, 1), Topic = "Topic1", Annotation = "Ann1" },
//        new() { Id = 2, Name = "Doc2", Title = "Title2", Year = new DateOnly(2023, 1, 1), Topic = "Topic2", Annotation = "Ann2" }
//    };

//        public Task<DTOdocumentV1> GetAsync(int? id, CancellationToken token)
//        {
//            var doc = _mockDocs.FirstOrDefault(d => d.Id == id);
//            if (doc == null) throw new KeyNotFoundException();
//            return Task.FromResult(doc);
//        }

//        public Task<IEnumerable<DTOdocumentV1>> GetAllAsync(CancellationToken cancellationToken)
//        {
//            return Task.FromResult<IEnumerable<DTOdocumentV1>>(_mockDocs);
//        }

//        public Task AddAsync(DTOdocumentV1 product, CancellationToken cancellationToken)
//        {
//            product.Id = _mockDocs.Max(d => d.Id) + 1;
//            _mockDocs.Add(product);
//            return Task.CompletedTask;
//        }

//        public Task UpdateAsync(DTOdocumentV1 product, CancellationToken cancellationToken)
//        {
//            var index = _mockDocs.FindIndex(d => d.Id == product.Id);
//            if (index == -1) throw new KeyNotFoundException();

//            _mockDocs[index] = product;
//            return Task.CompletedTask;
//        }

//        public Task DeleteAsync(int? id, CancellationToken cancellationToken)
//        {
//            var doc = _mockDocs.FirstOrDefault(d => d.Id == id);
//            if (doc == null) throw new KeyNotFoundException();

//            _mockDocs.Remove(doc);
//            return Task.CompletedTask;
//        }
//    }


//}

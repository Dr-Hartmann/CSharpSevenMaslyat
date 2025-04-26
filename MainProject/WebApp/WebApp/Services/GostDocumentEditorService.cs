using DTOmvp;

namespace WebApp.Services
{
    public class MockDocumentEditorService : IDocumentEditorService
    {
        private List<DTOdocumentV1> _mockDocs = new()
    {
        new() { Id = 1, Name = "Doc1", Title = "Title1", Year = new DateOnly(2022, 1, 1), Topic = "Topic1", Annotation = "Ann1" },
        new() { Id = 2, Name = "Doc2", Title = "Title2", Year = new DateOnly(2023, 1, 1), Topic = "Topic2", Annotation = "Ann2" }
    };

        public Task<DTOdocumentV1> GetAsync(int? id, CancellationToken token)
        {
            var doc = _mockDocs.FirstOrDefault(d => d.Id == id);
            if (doc == null) throw new KeyNotFoundException();
            return Task.FromResult(doc);
        }

        public Task<IEnumerable<DTOdocumentV1>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult<IEnumerable<DTOdocumentV1>>(_mockDocs);
        }

        public Task AddAsync(DTOdocumentV1 product, CancellationToken cancellationToken)
        {
            product.Id = _mockDocs.Max(d => d.Id) + 1;
            _mockDocs.Add(product);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(DTOdocumentV1 product, CancellationToken cancellationToken)
        {
            var index = _mockDocs.FindIndex(d => d.Id == product.Id);
            if (index == -1) throw new KeyNotFoundException();

            _mockDocs[index] = product;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int? id, CancellationToken cancellationToken)
        {
            var doc = _mockDocs.FirstOrDefault(d => d.Id == id);
            if (doc == null) throw new KeyNotFoundException();

            _mockDocs.Remove(doc);
            return Task.CompletedTask;
        }
    }


}

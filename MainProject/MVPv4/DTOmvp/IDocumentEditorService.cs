﻿namespace DTOmvp;

public interface IDocumentEditorService
{
    //Task<IEnumerable<DocumentV1>> GetAllAsync();
    //Task<DocumentV1> GetByIdAsync(int? id);
    //Task AddAsync(DocumentV1 product);
    //Task UpdateAsync(DocumentV1 product);
    //Task DeleteAsync(int? id);
    Task<DTOdocumentV1> GetAsync(int? id, CancellationToken token);
    Task<IEnumerable<DTOdocumentV1>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DTOdocumentV1 product);
}

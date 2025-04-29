using MVPv5.Core.Models;

namespace MVPv5.Core.Abstractions;

public interface IUserRepository
{
    Task<int> AddAsync(string nickname, string login, string password, byte accessRule, DateOnly dateCreation, IEnumerable<DocumentModel> documents, CancellationToken token);
    Task<int> DeleteById(int id, CancellationToken token);
    Task<IEnumerable<(UserModel User, string Error)>> GetAllAsync(CancellationToken token);
    Task<IEnumerable<(UserModel User, string Error)>> GetByFilterAsync(int lessThan, CancellationToken token);
    Task<(UserModel User, string Error)> GetByIdAsync(int id, CancellationToken token);
    Task<IEnumerable<(UserModel User, string Error)>> GetWithDocumentsAsync(CancellationToken token);
    Task<int> Update(int id, string nickname, string login, string password, byte accessRule, CancellationToken token);
}
using MVPv5.Core.Models;

namespace MVPv5.Core.Abstractions.v1;

public interface IUserRepository
{
    Task<int> AddAsync(string nickname, string login, string password, byte accessRule, DateOnly dateCreation, CancellationToken token);
    Task<int> DeleteById(int id, CancellationToken token);
    Task<IEnumerable<(UserModel User, string Error)>> GetAllAsync(CancellationToken token);
    //Task<IEnumerable<(UserModel User, string Error)>> GetByFilterAsync(int lessThan, CancellationToken token);
    Task<(UserModel User, string Error)> GetByIdAsync(int id, CancellationToken token);
    //Task<IEnumerable<(UserModel User, string Error)>> GetWithDocumentsAsync(CancellationToken token);
    Task<int> Update(int id, string nickname, string login, string password, byte accessRule, CancellationToken token);
    Task<bool> UpdatePassword(int id, string password, CancellationToken token);
}
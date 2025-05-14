using MVPv5.Core.Models;

namespace MVPv5.Core.Abstractions.v1;

public interface IUserRepository
{
    Task AddAsync(string nickname, string login, string password,
        byte accessRule, DateOnly dateCreation, CancellationToken token);
    Task UpdateNicknameAsync(int id, string nickname, CancellationToken token);
    Task UpdateLoginAsync(int id, string login, CancellationToken token);
    Task UpdatePasswordByLoginAsync(string login, string password, CancellationToken token);
    Task<(UserModel User, string Error)> GetByIdAsync(int id, CancellationToken token);
    Task<IEnumerable<(UserModel User, string Error)>> GetAllAsync(CancellationToken token);
    Task DeleteById(int id, CancellationToken token);
    Task<(UserModel User, string Error)> GetByLoginAsync(string login, CancellationToken token);
}
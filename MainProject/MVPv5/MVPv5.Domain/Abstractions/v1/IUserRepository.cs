using MVPv5.Domain.Models;

namespace MVPv5.Domain.Abstractions.v1;

public interface IUserRepository
{
    Task AddAsync(UserModel entity, CancellationToken token);
    Task UpdateNicknameByIdAsync(int id, string nickname, CancellationToken token);
    Task UpdateLoginByIdAsync(int id, string login, CancellationToken token);
    Task UpdatePasswordByLoginAsync(string login, string password, CancellationToken token);
    Task<(UserModel User, string Error)> GetByIdAsync(int id, CancellationToken token);
    Task<IEnumerable<(UserModel User, string Error)>> GetAllAsync(CancellationToken token);
    Task DeleteById(int id, CancellationToken token);
    Task<(UserModel User, string Error)> GetByLoginAsync(string login, CancellationToken token);
    Task<(UserModel User, string Error)> GetByLoginAndPasswordAsync(string login, string password, CancellationToken token);
}
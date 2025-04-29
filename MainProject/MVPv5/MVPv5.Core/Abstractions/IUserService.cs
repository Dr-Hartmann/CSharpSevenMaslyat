using MVPv5.Core.Models;

namespace MVPv5.Core.Abstractions;

public interface IUserService
{
    Task<int> CreateAsync(string nickname, string login, string password, byte accessRule, CancellationToken token);
    Task<UserModel> GetByIdAsync(int id, CancellationToken token);
    Task<IEnumerable<UserModel>> GetAllAsync(CancellationToken token);
    Task<bool> UpdatePasswordById(int id, string password, string confirmPassword, CancellationToken token);
}
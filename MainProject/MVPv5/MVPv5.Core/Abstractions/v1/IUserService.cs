using MVPv5.Core.Models;

namespace MVPv5.Core.Abstractions.v1;

public interface IUserService
{
    Task CreateAsync(string nickname, string login, string password,
        byte accessRule, CancellationToken token);
    Task<UserModel> GetByIdAsync(int id, CancellationToken token);
    Task<UserModel> GetByLoginAsync(string login, CancellationToken token);
    Task<IEnumerable<UserModel>> GetAllAsync(CancellationToken token);
    Task UpdateNicknameAsync(int id, string nickname, string confirmNickname, CancellationToken token);
    Task UpdateLoginAsync(int id, string login, string confirmLogin, CancellationToken token);
    Task UpdatePasswordByLogin(string login, string password, string confirmPassword, CancellationToken token);
    Task<UserModel> GetByLoginAndPasswordAsync(string login, string password, CancellationToken token);
    Task DeleteByIdAsync(int id, CancellationToken token);
}
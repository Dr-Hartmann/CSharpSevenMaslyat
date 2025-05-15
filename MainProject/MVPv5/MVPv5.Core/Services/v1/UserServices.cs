using MVPv5.Core.Abstractions.v1;
using MVPv5.Core.Models;

namespace MVPv5.Application.Services.v1;

public class UserService(IUserRepository repository) : IUserService
{
    public async Task CreateAsync(string nickname, string login, string password,
        byte accessRule, CancellationToken token)
    {
        await repository.AddAsync(nickname, login, password, accessRule, DateOnly.FromDateTime(DateTime.Now), token);
    }

    public async Task<UserModel> GetByIdAsync(int id, CancellationToken token)
    {
        var response = await repository.GetByIdAsync(id, token);

        if (response.Error != string.Empty)
        {
            throw new KeyNotFoundException($"Ошибка: {response.Error}");
        }

        return response.User;
    }

    public async Task<UserModel> GetByLoginAsync(string login, CancellationToken token)
    {
        var response = await repository.GetByLoginAsync(login, token);

        if (response.Error != string.Empty)
        {
            throw new KeyNotFoundException($"Ошибка: {response.Error}");
        }

        return response.User;
    }

    public async Task<UserModel> GetByLoginAndPasswordAsync(string login, string password, CancellationToken token)
    {
        var response = await repository.GetByLoginAndPasswordAsync(login, password, token);

        if (response.Error != string.Empty)
        {
            throw new KeyNotFoundException($"Ошибка: {response.Error}");
        }

        return response.User;
    }

    public async Task<IEnumerable<UserModel>> GetAllAsync(CancellationToken token)
    {
        var response = await repository.GetAllAsync(token);
        var errors = response.Select(r => r.Error != string.Empty);

        if (errors.Count() > 0)
        {
            System.Text.StringBuilder str = new();
            response.Select(r => r.Error).Select(item => str.Append($"| {item}"));
            throw new Exception($"Ошибка: {str.ToString()}");
        }

        return response.Select(l => l.User);
    }

    public async Task UpdateNicknameAsync(int id, string nickname, string confirmNickname, CancellationToken token)
    {
        if (!string.Equals(nickname, confirmNickname))
        {
            throw new Exception($"Никнеймы не совпадают");
        }

        await repository.UpdateNicknameAsync(id, nickname, token);
    }

    public async Task UpdateLoginAsync(int id, string login, string confirmLogin, CancellationToken token)
    {
        if (!string.Equals(login, confirmLogin))
        {
            throw new Exception($"Логины не совпадают");
        }

        await repository.UpdateLoginAsync(id, login, token);
    }

    public async Task UpdatePasswordByLogin(string login, string password, string confirmPassword, CancellationToken token)
    {
        if (!string.Equals(password, confirmPassword))
        {
            throw new Exception($"Пароли не совпадают");
        }

        await repository.UpdatePasswordByLoginAsync(login, password, token);
    }
}

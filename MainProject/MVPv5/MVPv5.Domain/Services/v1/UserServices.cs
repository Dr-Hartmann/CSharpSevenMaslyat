using Microsoft.AspNetCore.Identity;
using MVPv5.Domain.Abstractions.v1;
using MVPv5.Domain.Models;

namespace MVPv5.Domain.Services.v1;

public class UserService(IUserRepository repository) : IUserService
{
    public async Task CreateAsync(string nickname, string login, string password, byte accessRule, 
        DateOnly dateCreation, CancellationToken token)
    {
        var hashedPassword = new PasswordHasher<string>().HashPassword(login, password);
        await repository.AddAsync(UserModel.Create(nickname, login, hashedPassword, accessRule, dateCreation), token);
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
        // Зачем нам расхэширование???
        var hashedPassword = new PasswordHasher<string>().HashPassword(login, password);
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

        var errors = response.Where(r => !string.IsNullOrEmpty(r.Error)).ToList();
        if (errors.Any())
        {
            throw new Exception($"Ошибка: {string.Join(" | ", errors.Select(e => e.Error))}");
        }

        return response.Select(l => l.User);
    }

    public async Task UpdateNicknameAsync(int id, string nickname, string confirmNickname, CancellationToken token)
    {
        if (!string.Equals(nickname, confirmNickname))
        {
            throw new Exception($"Никнеймы не совпадают");
        }

        await repository.UpdateNicknameByIdAsync(id, nickname, token);
    }

    public async Task UpdateLoginAsync(int id, string login, string confirmLogin, CancellationToken token)
    {
        if (!string.Equals(login, confirmLogin))
        {
            throw new Exception($"Логины не совпадают");
        }

        await repository.UpdateLoginByIdAsync(id, login, token);
    }

    public async Task UpdatePasswordByLogin(string login, string password, string confirmPassword, CancellationToken token)
    {
        if (!string.Equals(password, confirmPassword))
        {
            throw new Exception($"Пароли не совпадают");
        }

        var hashedPassword = new PasswordHasher<string>().HashPassword(login, password);
        await repository.UpdatePasswordByLoginAsync(login, password, token);
    }

    public async Task DeleteByIdAsync(int id, CancellationToken token)
    {
        await repository.DeleteById(id, token);
    }
}

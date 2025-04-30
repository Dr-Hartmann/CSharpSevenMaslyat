using MVPv5.Core.Abstractions;
using MVPv5.Core.Models;

namespace MVPv5.Application.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    // Кэш пользователей по ID
    private readonly Dictionary<int, UserModel> _userCache = new();

    public async Task<int> CreateAsync(string nickname, string login, string password, byte accessRule, CancellationToken token)
    {
        return await userRepository.AddAsync(nickname, login, password, accessRule, DateOnly.FromDateTime(DateTime.Now), new List<DocumentModel>(), token);
    }

    public async Task<UserModel> GetByIdAsync(int id, CancellationToken token)
    {
        if (_userCache.TryGetValue(id, out var cachedUser))
        {
            return cachedUser;
        }

        var response = await userRepository.GetByIdAsync(id, token);

        if (response.Error != string.Empty)
        {
            throw new KeyNotFoundException($"Ошибка: {response.Error}");
        }

        _userCache[id] = response.User;
        return response.User;
    }

    public async Task<IEnumerable<UserModel>> GetAllAsync(CancellationToken token)
    {
        return (await userRepository.GetAllAsync(token)).Select(l => l.User);
    }

    public async Task<bool> UpdatePasswordById(int id, string password, string confirmPassword, CancellationToken token)
    {
        if (!string.Equals(password, confirmPassword))
        {
            throw new KeyNotFoundException($"Пароли не совпадают");
        }
        return await userRepository.UpdatePassword(id, password, token);
    }
}

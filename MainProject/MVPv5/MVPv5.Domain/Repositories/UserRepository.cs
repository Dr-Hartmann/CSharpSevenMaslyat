using Microsoft.EntityFrameworkCore;
using MVPv5.Domain.Abstractions.v1;
using MVPv5.Domain.Data;
using MVPv5.Domain.Entities;
using MVPv5.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace MVPv5.Domain.Repositories;

public class UserRepository(MVPv5DbContext dbContext) : IUserRepository
{
    public async Task AddAsync(UserModel model, CancellationToken token)
    {
        if (await dbContext.Users.AnyAsync(u => u.Login == model.Login, token))
        {
            throw new ValidationException("Такой пользователь уже существует");
        }

        await dbContext!.Users.AddAsync(new UserEntity
        {
            Login = model.Login,
            Nickname = model.Nickname,
            Password = model.Password,
            AccessRule = model.AccessRule,
            DateCreation = model.DateCreation,
        }, token);

        await dbContext.SaveChangesAsync(token);
    }

    public async Task UpdateNicknameByIdAsync(int id, string nickname, CancellationToken token)
    {
        await dbContext.Users
            .Where(user => user.Id == id)
            .ExecuteUpdateAsync(user => user
                .SetProperty(u => u.Nickname, nickname),
                token);

        await dbContext.SaveChangesAsync(token);
    }

    public async Task UpdateLoginByIdAsync(int id, string login, CancellationToken token)
    {
        await dbContext.Users
            .Where(user => user.Id == id)
            .ExecuteUpdateAsync(user => user
                .SetProperty(u => u.Login, login),
                token);

        await dbContext.SaveChangesAsync(token);
    }

    public async Task UpdatePasswordByLoginAsync(string login, string password, CancellationToken token)
    {
        await dbContext.Users
            .Where(user => user.Login == login)
            .ExecuteUpdateAsync(user => user
                .SetProperty(u => u.Password, password),
                token);

        await dbContext.SaveChangesAsync(token);
    }

    public async Task<(UserModel User, string Error)> GetByIdAsync(int id, CancellationToken token)
    {
        return GetUser(await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id == id, token));
    }

    public async Task<(UserModel User, string Error)> GetByLoginAsync(string login, CancellationToken token)
    {
        return GetUser(await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Login == login, token));
    }

    public async Task<(UserModel User, string Error)> GetByLoginAndPasswordAsync(string login, string password, CancellationToken token)
    {
        return GetUser(await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Login == login && user.Password == password, token));
    }

    public async Task<IEnumerable<(UserModel User, string Error)>> GetAllAsync(CancellationToken token)
    {
        return GetListOfUsers(await dbContext.Users
            .AsNoTracking()
            .ToListAsync(token));
    }

    public async Task DeleteById(int id, CancellationToken token)
    {
        var count = await dbContext.Users
            .Where(user => user.Id == id)
            .ExecuteDeleteAsync(token);

        await dbContext.SaveChangesAsync(token);

        if (count != 1)
        {
            throw new Exception($"Удалено {count} пользователей вместо 1");
        }
    }

    private (UserModel User, string Error) GetUser(UserEntity? response)
    {
        return UserModel.Create(response);
    }

    private IEnumerable<(UserModel User, string Error)> GetListOfUsers(IEnumerable<UserEntity>? response)
    {
        if (response == null) throw new Exception("Пустой лист в ответе");
        return response.Select(GetUser);
    }
}

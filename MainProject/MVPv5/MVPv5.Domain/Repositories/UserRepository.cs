using Microsoft.EntityFrameworkCore;
using MVPv5.Core.Abstractions.v1;
using MVPv5.Core.Models;
using MVPv5.Domain.Data;
using MVPv5.Domain.Entities;

namespace MVPv5.Domain.Repositories;

public class UserRepository(MVPv5DbContext dbContext) : IUserRepository
{

    /*  TODO - дополнить недостающие  */


    public async Task<int> AddAsync(string nickname, string login, string password, byte accessRule, DateOnly dateCreation, CancellationToken token)
    {
        if (dbContext!.Users.Where(u => u.Login == login).Count() > 0)
        {
            return -1;
        }

        await dbContext!.Users.AddAsync(new UserEntity
        {
            Nickname = nickname,
            Login = login,
            Password = password,
            AccessRule = accessRule,
            DateCreation = dateCreation,
        }, token);
        await dbContext.SaveChangesAsync(token);

        return dbContext!.Users.FirstOrDefaultAsync(u => u.Login == login, token).Id;
    }

    public async Task<int> Update(int id, string nickname, string login, string password, byte accessRule, CancellationToken token)
    {
        await dbContext.Users
            .Where(user => user.Id == id)
            .ExecuteUpdateAsync(user => user
                .SetProperty(u => u.Nickname, nickname)
                .SetProperty(u => u.Login, login)
                .SetProperty(u => u.Password, password)
                .SetProperty(u => u.AccessRule, accessRule), token);
        await dbContext.SaveChangesAsync(token);
        return id;
    }

    public async Task<bool> UpdatePassword(int id, string password, CancellationToken token)
    {
        await dbContext.Users
            .Where(user => user.Id == id)
            .ExecuteUpdateAsync(user => user
                .SetProperty(u => u.Password, password), token);
        await dbContext.SaveChangesAsync(token);
        return true;
    }

    /// <returns>Количество удалённых записей из БД.</returns>
    public async Task<int> DeleteById(int id, CancellationToken token)
    {
        var count = await dbContext.Users
            .Where(user => user.Id == id)
            .ExecuteDeleteAsync(token);
        await dbContext.SaveChangesAsync(token);
        return count;
    }

    public async Task<(UserModel User, string Error)> GetByIdAsync(int id, CancellationToken token)
    {
        return GetUser(await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id == id, token));
    }

    //public async Task<IEnumerable<(UserModel User, string Error)>> GetWithDocumentsAsync(CancellationToken token)
    //{
    //    return GetListOfUsers(await dbContext.Users
    //        .AsNoTracking()
    //        .Include(user => user.Documents)
    //        .ToListAsync(token));
    //}

    public async Task<IEnumerable<(UserModel User, string Error)>> GetAllAsync(CancellationToken token)
    {
        return GetListOfUsers(await dbContext.Users
            .AsNoTracking()
            .ToListAsync(token));
    }

    //public async Task<IEnumerable<(UserModel User, string Error)>> GetByFilterAsync(int lessThan, CancellationToken token)
    //{
    //    var query = dbContext!.Users.AsNoTracking();
    //    if (lessThan > 0)
    //    {
    //        query = query.Where(user => user.Documents!.Count() < lessThan);
    //    }

    //    return GetListOfUsers(await query.ToListAsync(token));
    //}


    private (UserModel User, string Error) GetUser(UserEntity? response)
    {
        if (response == null) throw new Exception("Пустой пользователь в ответе");
        return UserModel.Create(response.Id, response.Nickname, response.Login, response.Password, response.AccessRule, response.DateCreation);
    }

    private IEnumerable<(UserModel User, string Error)> GetListOfUsers(List<UserEntity>? response)
    {
        if (response == null) throw new Exception("Пустой лист в ответе");
        return response.Select(a => UserModel.Create(a.Id, a.Nickname, a.Login, a.Password, a.AccessRule, a.DateCreation));
    }
}

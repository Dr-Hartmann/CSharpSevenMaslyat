using MVPv5.Core.Abstractions;
using MVPv5.Core.Models;

namespace MVPv5.Application.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<int> CreateAsync(string nickname, string login, string password, byte accessRule, CancellationToken token)
    {
        return await userRepository.AddAsync(nickname, login, password, accessRule, DateOnly.FromDateTime(DateTime.Now), new List<DocumentModel>(), token);
    }

    //public async Task<User> GetAsync(int? id, CancellationToken token)
    //{
    //    var document = await dbContext!.Users
    //        .AsNoTracking().FirstOrDefaultAsync(m => m.Id == id, token);
    //    return new User
    //    {
    //        Id = document!.Id,
    //        Nickname = document!.Nickname,
    //        Login = document!.Login,
    //        Password = document!.Password,
    //        AccessRule = document!.AccessRule,
    //    };
    //}
}

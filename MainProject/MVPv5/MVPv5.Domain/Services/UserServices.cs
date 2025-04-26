using Microsoft.EntityFrameworkCore;
using MVPv5.Core.Models;
using MVPv5.Domain.Entities;

namespace MVPv5.Domain.Services;

public class UserService//(MVPv5Database dbContext)
{
    public async Task AddAsync(User product)
    {
        //await dbContext!.Users.AddAsync(new UserEntity // TODO - сделать конструктором
        //{
        //    Id = product!.Id,
        //    Nickname = product!.Nickname,
        //    Login = product!.Login,
        //    Password = product!.Password,
        //    AccessRule = product!.AccessRule,
        //});
        //await dbContext.SaveChangesAsync();
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

namespace MVPv5.Core.Abstractions;

public interface IUserService
{
    Task<int> CreateAsync(string nickname, string login, string password, byte accessRule, CancellationToken token);
}
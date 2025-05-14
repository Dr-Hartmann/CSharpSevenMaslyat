using MVPv5.Core.Abstractions.v1;

namespace MVPv5.Application.Services.v1;

public class DocumentService(IDocumentRepository repository) : IDocumentService
{
    //public async Task<int> CreateAsync(string nickname, string login, string password, byte accessRule, CancellationToken token)
    //{
    //    return await repository.AddAsync(nickname, login, password, accessRule, DateOnly.FromDateTime(DateTime.Now), token);
    //}

    //public async Task<UserModel> GetByIdAsync(int id, CancellationToken token)
    //{
    //    var response = await repository.GetByIdAsync(id, token);

    //    if (response.Error != string.Empty)
    //    {
    //        throw new KeyNotFoundException($"Ошибка: {response.Error}");
    //    }

    //    return response.User;
    //}

    //public async Task<IEnumerable<UserModel>> GetAllAsync(CancellationToken token)
    //{
    //    return (await repository.GetAllAsync(token)).Select(l => l.User);
    //}

    //public async Task<bool> UpdatePasswordById(int id, string password, string confirmPassword, CancellationToken token)
    //{
    //    if (!string.Equals(password, confirmPassword))
    //    {
    //        throw new KeyNotFoundException($"Пароли не совпадают");
    //    }

    //    return await repository.UpdatePassword(id, password, token);
    //}
}

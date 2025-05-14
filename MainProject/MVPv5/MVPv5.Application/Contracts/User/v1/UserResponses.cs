namespace MVPv5.Application.Contracts.User.v1;

public record UserReadResponse(
    int id,
    string nickname,
    string login,
    string password,
    byte accessRule,
    DateOnly dateCreation
);
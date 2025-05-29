namespace MVPv5.Application.Contracts.User.v1;

public record UserReadResponse(
    int Id,
    string Nickname,
    string Login,
    string Password,
    byte AccessRule,
    DateOnly DateCreation
);
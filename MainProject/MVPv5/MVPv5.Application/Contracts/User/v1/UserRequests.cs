﻿using System.ComponentModel.DataAnnotations;

namespace MVPv5.Application.Contracts.User.v1;


#pragma warning disable CS8618 
public class UserCreateRequest
{
    [Required, StringLength(20, MinimumLength = 2)]
    public string Nickname { get; set; }

    [Required, StringLength(50, MinimumLength = 10, ErrorMessage = "Не короче 5 и не больше 30 символов"),
        RegularExpression("^[A-Za-z0-9_-]+@chsu.ru$", ErrorMessage = "Должен быть почтой ЧГУ")]
    public string Login { get; set; }

    [Required, StringLength(30, MinimumLength = 5, ErrorMessage = "Не короче 5 и не больше 30 символов")]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string PasswordConfirm { get; set; }
}
#pragma warning restore CS8618 

public class UserLoginRequest
{
#pragma warning disable CS8618 
    [Required, StringLength(50, MinimumLength = 10, ErrorMessage = "Не короче 5 и не больше 30 символов"),
        RegularExpression("^[A-Za-z0-9_-]+@chsu.ru$", ErrorMessage = "Должен быть почтой ЧГУ")]
    public string Login { get; set; }

    [Required, StringLength(30, MinimumLength = 5, ErrorMessage = "Не короче 5 и не больше 30 символов")]
    public string Password { get; set; }
#pragma warning restore CS8618 
}


public class UserPatchPasswordRequest
{
    [Required, StringLength(50, MinimumLength = 10)]
    public required string Login { get; set; }

    [StringLength(30, MinimumLength = 5)]
    public required string Password { get; set; }

    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public required string PasswordConfirm { get; set; }
}


//public class UserPutPasswordRequest
//{
//    [Required]
//    public int Id { get; set; }

//    [StringLength(20, MinimumLength = 2)]
//    public string? Nickname { get; set; }

//    [StringLength(50, MinimumLength = 10)]
//    public string? Login { get; set; }

//    [StringLength(30, MinimumLength = 5)]
//    public string? Password { get; set; }

//    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
//    public string? PasswordConfirm { get; set; }
//}


//[Remote(action: "CheckEmail", controller: "DocumentV1", ErrorMessage = "Имя уже используется")]
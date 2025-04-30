using Microsoft.AspNetCore.Mvc;
using MVPv5.Application.Contracts;
using MVPv5.Core.Abstractions;
using MVPv5.Core.Models;
using MVPv5.Application.Services;

namespace MVPv5.API.Controllers;

// TDOD - разработать новые контроллеры

[ApiController]
[Route("[controller]/[action]")]
public class UserController(IUserService service) : ControllerBase
{
    //private readonly IUserService service;
    //public UserController(IUserService service)
    //{
    //    this.service = service;
    //}

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] UserCreateRequest user, CancellationToken token = default)
    {
        try
        {
            var id = await service.CreateAsync(user.Nickname, user.Login, user.Password, 30, token);
            if (id == -1) return BadRequest("Такой пользователь уже существует");
            return Ok();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserGetResponse>> Get(int id, CancellationToken token = default)
    {
        try
        {
            var response = await service.GetByIdAsync(id, token);
            return new UserGetResponse()
            {
                Id = response.Id,
                Nickname = response.Nickname,
                Login = response.Login,
                AccessRule = response.AccessRule,
                DateCreation = response.DateCreation,
                Password = response.Password,
                Documents = response.Documents
            };
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserGetResponse>>> GetAll(CancellationToken token = default)
    {
        return Ok((await service.GetAllAsync(token)).Select(response => new UserGetResponse()
        {
            Id = response.Id,
            Nickname = response.Nickname,
            Login = response.Login,
            AccessRule = response.AccessRule,
            DateCreation = response.DateCreation,
            Password = response.Password,
            Documents = response.Documents
        }));
    }

    [HttpPatch]
    public async Task<ActionResult<bool>> UpdatePassword(int id, [FromBody] UserPatchRequest user, CancellationToken token = default)
    {
        return await service.UpdatePasswordById(id, user.Password!, user.PasswordConfirm!, token);
    }

    [HttpGet("export/{id}")]
    public async Task<IActionResult> ExportUser(int id, CancellationToken token = default)
    {
        var user = await service.GetByIdAsync(id, token);
        var json = JsonHelper.Serialize(user);
        return File(System.Text.Encoding.UTF8.GetBytes(json), "application/json", $"user_{id}.json");
    }

    [HttpPost("import")]
    public async Task<IActionResult> ImportUser([FromBody] string json, CancellationToken token = default)
    {
        var user = JsonHelper.Deserialize<UserModel>(json);
        var id = await service.CreateAsync(user.Nickname, user.Login, user.Password, user.AccessRule, token);
        if (id == -1) return BadRequest("Такой пользователь уже существует");
        return Ok();
    }
}

using Microsoft.AspNetCore.Mvc;
using MVPv5.Core.Abstractions.v1;
using MVPv5.Application.Contracts.User.v1;

namespace MVPv5.API.Controllers.v1;

// TDOD - разработать новые контроллеры

[ApiController]
[Route("v1/[controller]/[action]")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] UserCreateRequest user, CancellationToken token = default)
    {
        if (!ModelState.IsValid)
        {
            // TODO - logger
            return BadRequest(ModelState);
        }

        try
        {
            var id = await service.CreateAsync(user.Nickname, user.Login, user.Password, 30, token);
            if (id == -1) return BadRequest("Такой пользователь уже существует");
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
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
                Password = response.Password
            };
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Data);
        }
    }


    // TODO - другие Get, но с другим набором параметров.
    // Как сделать несколько разных гетов без переименования? (я ответ знаю)


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
            Password = response.Password
        }));
    }

    [HttpPatch]
    public async Task<ActionResult<bool>> UpdatePassword([FromBody] UserPatchRequest user, CancellationToken token = default)
    {
        if (!ModelState.IsValid)
        {
            // TODO - logger
            return BadRequest(ModelState);
        }

        //if(не заполнены поля) return BadRequest(); // TODO

        return await service.UpdatePasswordById(user.Id, user.Password!, user.PasswordConfirm!, token);
    }

    //[HttpGet("export/{id}")]
    //public async Task<IActionResult> ExportUser(int id, CancellationToken token = default)
    //{
    //    var user = await service.GetByIdAsync(id, token);
    //    var json = JsonHelper.Serialize(user);
    //    return File(System.Text.Encoding.UTF8.GetBytes(json), "application/json", $"user_{id}.json");
    //}

    //[HttpPost("import")]
    //public async Task<IActionResult> ImportUser([FromBody] string json, CancellationToken token = default)
    //{
    //    var user = JsonHelper.Deserialize<UserModel>(json);
    //    var id = await service.CreateAsync(user.Nickname, user.Login, user.Password, user.AccessRule, token);
    //    if (id == -1) return BadRequest("Такой пользователь уже существует");
    //    return Ok();
    //}
}

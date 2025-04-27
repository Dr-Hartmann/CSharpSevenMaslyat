using Microsoft.AspNetCore.Mvc;
using MVPv5.Application.Contracts;
using MVPv5.Core.Abstractions;

namespace MVPv5.API.Controllers;

// TDOD - разработать новые контроллеры

[ApiController]
[Route("[controller]/[action]")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] UserCreateRequest user, CancellationToken token)
    {
        try
        {
            int id = await service.CreateAsync(user.Nickname, user.Login, user.Password, 30, token);
            if (id == -1) return BadRequest("Такой пользователь уже существует");
            return Ok();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    //[HttpGet("{id}")]
    //public async Task<ActionResult<User>> Get(int? id, CancellationToken cancellationToken)
    //{
    //    try
    //    {
    //        return await service.GetAsync(id, cancellationToken);
    //    }
    //    catch (KeyNotFoundException)
    //    {
    //        return NotFound();
    //    }
    //}
}

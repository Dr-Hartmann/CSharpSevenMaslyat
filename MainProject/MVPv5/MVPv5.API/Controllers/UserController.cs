using Microsoft.AspNetCore.Mvc;
using MVPv5.API.Services;
using MVPv5.Core.Models;

namespace MVPv5.API.Controllers;

// TODO - заставить работать
// TDOD - разработать новые контроллеры

[ApiController]
[Route("[controller]/[action]")]
public class UserController(UserService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create(User doc)
    {
        return Ok();
        try
        {
            await service.AddAsync(doc);
            return Ok();
        }
        catch (KeyNotFoundException)
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

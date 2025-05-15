using Microsoft.AspNetCore.Mvc;
using MVPv5.Core.Abstractions.v1;
using MVPv5.Application.Contracts.User.v1;

namespace MVPv5.API.Controllers.v1;

[ApiController]
[Area("v1")]
[Route("[controller]")]
public class UserController(IUserService service, ILogger<UserController> logger) : ControllerBase
{
    [HttpPost("create")]
    public async Task<ActionResult> Add([FromBody] UserCreateRequest user, CancellationToken token = default)
    {
        if (!ModelState.IsValid)
        {
            logger.LogError(ModelState.ToString());
            return ValidationProblem(ModelState);
        }

        try
        {
            await service.CreateAsync(user.Nickname, user.Login, user.Password, 30, token);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("read/{id:int}")]
    public async Task<ActionResult<UserReadResponse>> Get(int id, CancellationToken token = default)
    {
        try
        {
            var response = await service.GetByIdAsync(id, token);
            return new UserReadResponse(
                    response.Id,
                    response.Nickname,
                    response.Login,
                    response.Password,
                    response.AccessRule,
                    response.DateCreation);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogError(ex.Message);
            return NotFound(ex.Message);
        }
    }

    [HttpGet("read/{login}")]
    public async Task<ActionResult<UserReadResponse>> Get(string login, CancellationToken token = default)
    {
        try
        {
            var response = await service.GetByLoginAsync(login, token);
            return new UserReadResponse(
                    response.Id,
                    response.Nickname,
                    response.Login,
                    response.Password,
                    response.AccessRule,
                    response.DateCreation);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("check")]
    public async Task<ActionResult<UserReadResponse>> Get([FromBody] UserLoginRequest request, CancellationToken token = default)
    {
        try
        {
            var response = await service.GetByLoginAndPasswordAsync(request.Login, request.Password, token);
            return new UserReadResponse(
                    response.Id,
                    response.Nickname,
                    response.Login,
                    response.Password,
                    response.AccessRule,
                    response.DateCreation);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("read-all")]
    public async Task<ActionResult<IEnumerable<UserReadResponse>>> Get(CancellationToken token = default)
    {
        try
        {
            return Ok((await service.GetAllAsync(token)).Select(response =>
                new UserReadResponse(
                    response.Id, 
                    response.Nickname, 
                    response.Login,
                    response.Password,
                    response.AccessRule,
                    response.DateCreation)));
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return Problem(ex.Message);
        }
    }

    [HttpPatch]
    public async Task<IActionResult> UpdatePassword([FromBody] UserPatchPasswordRequest user, CancellationToken token = default)
    {
        if (!ModelState.IsValid)
        {
            logger.LogError(ModelState.ToString());
            return ValidationProblem(ModelState);
        }

        try
        {
            await service.UpdatePasswordByLogin(user.Login, user.Password, user.PasswordConfirm, token);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }

        return Created();
    }
}

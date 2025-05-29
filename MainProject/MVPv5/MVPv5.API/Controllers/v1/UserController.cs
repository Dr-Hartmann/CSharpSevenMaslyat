using Microsoft.AspNetCore.Mvc;
using MVPv5.Application.Contracts.User.v1;
using MVPv5.Domain.Abstractions.v1;
using MVPv5.Domain.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace MVPv5.API.Controllers.v1;

[ApiController]
[Area("v1")]
[Route("[controller]")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Add([FromBody] UserCreateRequest user, CancellationToken token = default)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }
        await service.CreateAsync(user.Nickname, user.Login, user.Password, 30, DateOnly.FromDateTime(DateTime.Now), token);
        return Created();
    }

    //[HttpGet("read/{id:int}")]
    [HttpGet("read")]
    [Authorize]
    [ProducesResponseType(typeof(UserReadResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserReadResponse>> Get(CancellationToken token = default)
    {
        var response = await service.GetByIdAsync(Convert.ToInt32(ClaimTypes.NameIdentifier), token);
        if (response is null)
        {
            return NotFound();
        }
        return ModelToResponse(response);
    }

    //[HttpGet("read/{login}")]
    //[Authorize]
    //[ProducesResponseType(typeof(UserReadResponse), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<UserReadResponse>> Get(string login, CancellationToken token = default)
    //{
    //    var response = await service.GetByLoginAsync(login, token);
    //    if (response is null)
    //    {
    //        return NotFound();
    //    }
    //    return ModelToResponse(response);
    //}

    [HttpPost("check")]
    [ProducesResponseType(typeof(UserReadResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserReadResponse>> Get([FromBody] UserLoginRequest request, CancellationToken token = default)
    {
        var response = await service.GetByLoginAndPasswordAsync(request.Login, request.Password, token);
        if (response is null)
        {
            return Unauthorized();
        }
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, response.Id.ToString()),
            new Claim(ClaimTypes.Email, response.Login),
            new Claim(ClaimTypes.Name, response.Nickname),
            new Claim(ClaimTypes.Role, response.AccessRule.ToString())
        };

        await HttpContext.SignInAsync("Cookies",
            new(new ClaimsIdentity(claims, "Cookies")),
            new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
            });

        return ModelToResponse(response);
    }

    //[HttpGet("read-all")]
    //[ProducesResponseType(typeof(IEnumerable<UserReadResponse>), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //public async Task<ActionResult<IEnumerable<UserReadResponse>>> Get(CancellationToken token = default)
    //{
    //    var result = await service.GetAllAsync(token);
    //    if (result.IsNullOrEmpty())
    //    {
    //        return NotFound();
    //    }
    //    return Ok(result.Select(ModelToResponse));
    //}

    [HttpPatch]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePassword([FromBody] UserPatchPasswordRequest user, CancellationToken token = default)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }
        await service.UpdatePasswordByLogin(user.Login, user.Password, user.PasswordConfirm, token);
        return Created();
    }

    [HttpDelete("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id, CancellationToken token = default)
    {
        await service.DeleteByIdAsync(id, token);
        return NoContent();
    }

    private static UserReadResponse ModelToResponse(UserModel model)
    {
        return new(
            model.Id,
            model.Nickname,
            model.Login,
            model.Password,
            model.AccessRule,
            model.DateCreation
        );
    }
}

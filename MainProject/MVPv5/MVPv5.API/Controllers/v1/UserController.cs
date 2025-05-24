using Microsoft.AspNetCore.Mvc;
using MVPv5.Core.Abstractions.v1;
using MVPv5.Application.Contracts.User.v1;

namespace MVPv5.API.Controllers.v1;

[ApiController]
[Area("v1")]
[Route("[controller]")]
public class UserController(IUserService service/*, ILogger<UserController> logger*/) : ControllerBase
{
    /// <summary>
    /// Создаёт нового пользователя.
    /// </summary>
    /// <returns>Код 201 при успешном создании.</returns>
    /// <response code="201">Пользователь успешно создан.</response>
    /// <response code="400">Ошибка валидации.</response>
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Add([FromBody] UserCreateRequest user, CancellationToken token = default)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        await service.CreateAsync(user.Nickname, user.Login, user.Password, 30, token);
        return Created();
    }

    /// <summary>
    /// Возвращает пользователя по id.
    /// </summary>
    /// <returns>Данные пользователя.</returns>
    /// <response code="200">Пользователь найден.</response>
    /// <response code="404">Пользователь не найден.</response>
    [HttpGet("read/{id:int}")]
    [ProducesResponseType(typeof(UserReadResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserReadResponse>> Get(int id, CancellationToken token = default)
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

    /// <summary>
    /// Возвращает пользователя по логину.
    /// </summary>
    /// <returns>Данные пользователя.</returns>
    /// <response code="200">Пользователь найден.</response>
    /// <response code="404">Пользователь не найден.</response>
    [HttpGet("read/{login}")]
    [ProducesResponseType(typeof(UserReadResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserReadResponse>> Get(string login, CancellationToken token = default)
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

    /// <summary>
    /// Проверяет логин и пароль пользователя.
    /// </summary>
    /// <returns>Данные пользователя при успешной проверке.</returns>
    /// <response code="200">Успешная проверка.</response>
    /// <response code="401">Неверный логин или пароль.</response>
    [HttpPost("check")]
    [ProducesResponseType(typeof(UserReadResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserReadResponse>> Get([FromBody] UserLoginRequest request, CancellationToken token = default)
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

    /// <summary>
    /// Возвращает всех пользователей.
    /// </summary>
    /// <returns>Список пользователей.</returns>
    /// <response code="200">Успешно получен список.</response>
    [HttpGet("read-all")]
    [ProducesResponseType(typeof(IEnumerable<UserReadResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserReadResponse>>> Get(CancellationToken token = default)
    {
        var result = await service.GetAllAsync(token);

        return Ok(result.Select(response => new UserReadResponse(
            response.Id,
            response.Nickname,
            response.Login,
            response.Password,
            response.AccessRule,
            response.DateCreation)));
    }

    /// <summary>
    /// Обновляет пароль пользователя.
    /// </summary>
    /// <returns>Код 201 при успешном обновлении.</returns>
    /// <response code="201">Пароль успешно обновлён.</response>
    /// <response code="400">Ошибка валидации.</response>
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePassword([FromBody] UserPatchPasswordRequest user, CancellationToken token = default)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        await service.UpdatePasswordByLogin(user.Login, user.Password, user.PasswordConfirm, token);
        return Created();
    }

    /// <summary>
    /// Удаляет пользователя по ID.
    /// </summary>
    /// <returns>Код 204 при успешном удалении.</returns>
    /// <response code="204">Пользователь успешно удалён.</response>
    [HttpDelete("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id, CancellationToken token = default)
    {
        await service.DeleteByIdAsync(id, token);
        return NoContent();
    }
}

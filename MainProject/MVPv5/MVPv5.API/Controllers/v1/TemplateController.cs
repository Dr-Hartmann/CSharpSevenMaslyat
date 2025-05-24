using Microsoft.AspNetCore.Mvc;
using MVPv5.Application.Contracts.Template.v1;
using MVPv5.Core.Abstractions.v1;
using MVPv5.Core.Models;

namespace MVPv5.API.Controllers.v1;

[ApiController]
[Area("v1")]
[Route("[controller]")]
public class TemplateController(ITemplateService service/*, ILogger<TemplateController> logger*/) : ControllerBase
{
    private static TemplateReadResponse ModelToResponse(TemplateModel m) =>
        new(
            m.Id, 
            m.Name, 
            m.Type, 
            m.DateCreation, 
            m.Content, 
            m.ContentType, 
            m.Tags
        );

    /// <summary>
    /// Создает новый шаблон.
    /// </summary>
    /// <returns>HTTP с кодом-ответом 201 при успешном создании, либо кодом-ответом ошибки.</returns>
    /// <response code="201">Шаблон успешно создан.</response>
    /// <response code="400">Ошибка валидации входных данных.</response>
    /// <response code="409">Шаблон с таким именем уже существует.</response>
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Add([FromBody] TemplateCreateRequest t, CancellationToken token = default)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        await service.AddAsync(t.Name, t.Type, DateOnly.FromDateTime(DateTime.Now), t.Content, t.ContentType, t.Tags, token);
        return Created();
    }

    /// <summary>
    /// Получает шаблон по его id.
    /// </summary>
    /// <returns>Шаблон, если найден.</returns>
    /// <response code="200">Шаблон найден.</response>
    /// <response code="404">Шаблон не найден.</response>
    [HttpGet("read/{id:int}")]
    [ProducesResponseType(typeof(TemplateReadResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TemplateReadResponse>> Get(int id, CancellationToken token = default)
    {
        var resp = await service.GetByIdAsync(id, token);
        return Ok(ModelToResponse(resp));
    }

    /// <summary>
    /// Получает все шаблоны.
    /// </summary>
    /// <returns>Список всех шаблонов.</returns>
    /// <response code="200">Список шаблонов получен.</response>
    /// <response code="404">Шаблоны не найдены.</response>
    [HttpGet("read-all")]
    [ProducesResponseType(typeof(IEnumerable<TemplateReadResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<TemplateReadResponse>>> GetAll(CancellationToken token = default)
    {
        var temlps = await service.GetAllAsync(token);

        if (!temlps.Any())
            return NotFound();

        return Ok(temlps.Select(ModelToResponse));
    }

    /// <summary>
    /// Частично обновляет шаблон.
    /// </summary>
    /// <returns>HTTP с кодом-ответом 201 при успешном обновлении.</returns>
    /// <response code="201">Шаблон успешно обновлен.</response>
    /// <response code="400">Ошибка валидации входных данных.</response>
    [HttpPatch("update")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Patch([FromBody] TemplatePatchRequest request, CancellationToken token = default)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        await service.PatchAsync(request.Id, request.Name, request.Type,
            request.Content, request.ContentType, request.Tags, token);

        return Created();
    }

    /// <summary>
    /// Полностью обновляет шаблон.
    /// </summary>
    /// <returns>HTTP с кодом-ответом 201 при успешном обновлении.</returns>
    /// <response code="201">Шаблон успешно обновлен.</response>
    /// <response code="400">Ошибка валидации входных данных.</response>
    [HttpPut("refresh")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] TemplateUpdateRequest request, CancellationToken token = default)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        await service.UpdateAsync(request.Id, request.Name, request.Type,
            DateOnly.FromDateTime(DateTime.Now), request.Content, request.ContentType, request.Tags, token);

        return Created();
    }

    /// <summary>
    /// Удаляет шаблон по данным из тела запроса.
    /// </summary>
    /// <returns>HTTP 204 если успешно.</returns>
    /// <response code="204">Шаблон успешно удален.</response>
    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete([FromBody] TemplateDeleteRequest request, CancellationToken token)
    {
        await service.DeleteByIdAsync(request.Id, token);
        return NoContent();
    }

    /// <summary>
    /// Удаляет шаблон по id.
    /// </summary>
    /// <returns>HTTP 204 если успешно.</returns>
    /// <response code="204">Шаблон успешно удален.</response>
    [HttpDelete("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteById(int id, CancellationToken token)
    {
        await service.DeleteByIdAsync(id, token);
        return NoContent();
    }

    /// <summary>
    /// Скачивает шаблон на основе переданного содержимого.
    /// </summary>
    /// <returns>Файл, если найден.</returns>
    /// <response code="200">Файл успешно скачан.</response>
    /// <response code="404">Файл не найден.</response>
    [HttpPost("download")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Download([FromBody] TemplateDownloadRequest file)
    {
        if (file.content == null)
            return NotFound("Файл не найден");

        return File(file.content, file.contentType, file.name, true);
    }

    /// <summary>
    /// Скачивает шаблон по идентификатору.
    /// </summary>
    /// <returns>Файл, если найден.</returns>
    /// <response code="200">Файл успешно скачан.</response>
    /// <response code="404">Файл не найден.</response>
    [HttpGet("download")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DownloadByID([FromQuery] int id, CancellationToken token)
    {
        var template = await service.GetByIdAsync(id, token);

        if (template.Content == null)
            return NotFound("Файл не найден");

        return File(template.Content, template.ContentType, template.Name, true);
    }
}

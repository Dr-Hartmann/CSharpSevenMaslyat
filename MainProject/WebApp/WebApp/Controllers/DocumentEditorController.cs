using DTOmvp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services;

namespace MVPv4.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class DocumentEditorController(IDocumentEditorService docEditorService, AuditService auditService) : Controller
{
    [Authorize]
    [HttpGet("All")]
    public IActionResult GetAll() => Ok("Success");

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<DTOdocumentV1>> Get(int? id, CancellationToken cancellationToken)
    {
        try
        {
            // Аудит получения документа
            await auditService.AuditAsync($"Получение документа с id={id}", User?.Identity?.Name ?? "anonymous", cancellationToken);
            return await docEditorService.GetAsync(id, cancellationToken);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<DTOdocumentV1>>> All(CancellationToken cancellationToken)
    {
        try
        {
            // Аудит получения всех документов
            await auditService.AuditAsync("Получение всех документов", User?.Identity?.Name ?? "anonymous", cancellationToken);
            var doc = await docEditorService.GetAllAsync(cancellationToken);
            return Ok(doc);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [Authorize]
    //[ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(DTOdocumentV1 doc, CancellationToken cancellationToken)
    {
        // Проверяем валидность полученных данных согласно аннотациям модели
        if (!ModelState.IsValid)
        {
            // Если данные невалидны, возвращаем ошибку 400 с описанием ошибок
            return BadRequest(ModelState);
        }
        try
        {
            // Аудит создания документа
            await auditService.AuditAsync($"Создание документа: {doc.Name}", User?.Identity?.Name ?? "anonymous", HttpContext.RequestAborted);
            // Если данные валидны, добавляем документ через сервис
            await docEditorService.AddAsync(doc, cancellationToken);
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update(DTOdocumentV1 doc, CancellationToken cancellationToken)
    {
        // Проверяем валидность полученных данных согласно аннотациям модели
        if (!ModelState.IsValid)
        {
            // Если данные невалидны, возвращаем ошибку 400 с описанием ошибок
            return BadRequest(ModelState);
        }
        try
        {
            // Аудит обновления документа
            await auditService.AuditAsync($"Обновление документа: {doc.Id}", User?.Identity?.Name ?? "anonymous", HttpContext.RequestAborted);
            // Если данные валидны, обновляем документ через сервис
            await docEditorService.UpdateAsync(doc, cancellationToken);
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken)
    {
        try
        {
            // Аудит удаления документа
            await auditService.AuditAsync($"Удаление документа: {id}", User?.Identity?.Name ?? "anonymous", HttpContext.RequestAborted);
            await docEditorService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    // TODO
    /* Дописать контроллер
     * Создать новые контроллеры по други путям
     * Внедрить другие сервисы
     * Внедрить токены отмены в сервисы
     */
}
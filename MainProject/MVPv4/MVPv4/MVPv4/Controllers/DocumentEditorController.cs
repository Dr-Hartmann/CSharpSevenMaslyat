using DTOmvp;
using Microsoft.AspNetCore.Mvc;

namespace MVPv4.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class DocumentEditorController(IDocumentEditorService docEditorService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<DTOdocumentV1>> Get(int? id, CancellationToken cancellationToken)
    {
        try
        {
            return await docEditorService.GetAsync(id, cancellationToken);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DTOdocumentV1>>> All(CancellationToken cancellationToken)
    {
        try
        {
            var doc = await docEditorService.GetAllAsync(cancellationToken);
            return Ok(doc);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    //[ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(DTOdocumentV1 doc)
    {
        try
        {
            await docEditorService.AddAsync(doc);
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MVPv5.Application.Contracts.Template.v1;
using MVPv5.Core.Abstractions.v1;

namespace MVPv5.API.Controllers.v1;

[ApiController]
[Area("v1")]
[Route("[controller]")]
public class TemplateController(ITemplateService service, ILogger<TemplateController> logger) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Add([FromBody] TemplateCreateRequest t, CancellationToken token = default)
    {
        if (!ModelState.IsValid)
        {
            logger.LogError(ModelState.ToString());
            return ValidationProblem(ModelState);
        }

        try
        {
            await service.AddAsync(t.Name, t.Type, DateOnly.FromDateTime(DateTime.Now), t.Content, t.ContentType, t.Tags, token);
            return Created();
        }
        catch (Exception ex)
        {
            logger.LogError(ModelState.ToString());
            return Problem(detail: ex.Message);
        }
    }

    [HttpGet("read/{id:int}")]
    public async Task<ActionResult<TemplateReadResponse>> Get(int id, CancellationToken token = default)
    {
        try
        {
            var r = await service.GetByIdAsync(id, token);
            return new TemplateReadResponse(
                r.Id,
                r.Name,
                r.Type,
                r.DateCreation,
                r.Content,
                r.ContentType,
                r.Tags);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Data);
        }
    }

    [HttpGet("read-all")]
    public async Task<ActionResult<IEnumerable<TemplateReadResponse>>> GetAll(CancellationToken token = default)
    {
        try
        {
            return Ok((await service.GetAllAsync(token)).Select(
                r => new TemplateReadResponse(
                    r.Id,
                    r.Name,
                    r.Type,
                    r.DateCreation,
                    r.Content,
                    r.ContentType,
                    r.Tags)));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Data);
        }
    }

    // TODO - разные поля или целиком
    //[HttpPatch("update")]
    //[HttpPut("refresh")]

    //[HttpDelete("delete")]

    [HttpGet("download")]
    public IActionResult Download([FromBody] TemplateDownloadRequest file)
    {
        if (file.content == null)
            return NotFound();

        // TODO - проверить
        return File(file.content, file.contentType, file.name, DateTime.Now, new EntityTagHeaderValue("<doc>"), true);
    }
}

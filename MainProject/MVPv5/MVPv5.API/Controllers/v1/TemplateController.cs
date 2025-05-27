using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MVPv5.Application.Contracts.Template.v1;
using MVPv5.Domain.Abstractions.v1;
using MVPv5.Domain.Models;

namespace MVPv5.API.Controllers.v1;

[ApiController]
[Area("v1")]
[Route("[controller]")]
public class TemplateController(ITemplateService service) : ControllerBase
{
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Add([FromBody] TemplateCreateRequest request, CancellationToken token = default)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }
        await service.AddAsync(TemplateModel.Create(request.Name, request.Type, DateOnly.FromDateTime(DateTime.Now),
            request.Content, request.ContentType, request.Tags), token);
        return Created();
    }

    [HttpGet("read/{id:int}")]
    [ProducesResponseType(typeof(TemplateReadResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TemplateReadResponse>> Get(int id, CancellationToken token = default)
    {
        var response = await service.GetByIdAsync(id, token);
        if (response is null)
        {
            return NotFound();
        }
        return Ok(response);
    }

    [HttpGet("read-all")]
    [ProducesResponseType(typeof(IEnumerable<TemplateReadResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<TemplateReadResponse>>> GetAll(CancellationToken token = default)
    {
        var result = await service.GetAllAsync(token);
        if (result.IsNullOrEmpty())
        {
            return NotFound();
        }
        return Ok(result.Select(ModelToResponse));
    }

    // TODO ...

   [HttpPatch("update")]
   [ProducesResponseType(StatusCodes.Status201Created)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Patch([FromBody] TemplatePatchRequest request, CancellationToken token = default)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }
        await service.UpdateTagsAsync(request.Id, request.Tags, token);
        await service.UpdateNameAsync(request.Id, request.Name, token);
        await service.UpdateContentAndContentTypeAsync(request.Id, request.Content, request.ContentType, token);
        return Created();
    }

    //[HttpPut("refresh")]
    //[ProducesResponseType(StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> Update([FromBody] TemplateUpdateRequest request, CancellationToken token = default)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return ValidationProblem(ModelState);
    //    }
    //    await service.UpdateAsync(request.Id, TemplateModel.Create(request.Name, request.Type, request.Content, request.ContentType, request.Tags), token);
    //    return Created();
    //}

    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete([FromBody] TemplateDeleteRequest request, CancellationToken token)
    {
        await service.DeleteByIdAsync(request.Id, token);
        return NoContent();
    }

    [HttpDelete("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteById(int id, CancellationToken token)
    {
        await service.DeleteByIdAsync(id, token);
        return NoContent();
    }

    [HttpPost("download")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Download([FromBody] TemplateDownloadRequest file)
    {
        if (file.Content == null)
        {
            return NotFound("Файл не найден");
        }
        return File(file.Content, file.ContentType, file.Name, true);
    }

    [HttpGet("download")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DownloadByID([FromQuery] int id, CancellationToken token)
    {
        var template = await service.GetByIdAsync(id, token);
        if (template.Content == null)
        {
            return NotFound("Файл не найден");
        }
        return File(template.Content, template.ContentType, template.Name, true);
    }

    private static TemplateReadResponse ModelToResponse(TemplateModel m) => new(
        m.Id,
        m.Name,
        m.Type,
        m.DateCreation,
        m.Content,
        m.ContentType,
        m.Tags
    );
}

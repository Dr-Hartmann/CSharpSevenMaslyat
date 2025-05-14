using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVPv5.Application.Contracts;
using MVPv5.Application.Contracts.User.v1;
using MVPv5.Core.Abstractions.v1;

namespace MVPv5.API.Controllers.v1;

[ApiController]
[Route("v1/[controller]/[action]")]
public class TemplateController(ITemplateService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] TemplateCreateRequest user, CancellationToken token = default)
    {
        if (!ModelState.IsValid)
        {
            // TODO - logger
            return BadRequest(ModelState);
        }

        try
        {
            var id = await service.CreateAsync(user.Name, user.Type, DateOnly.FromDateTime(DateTime.Now), user.Content, token);
            if (id == -1) return BadRequest("Такой шаблон уже существует");
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TemplateGetResponse>> Get(int id, CancellationToken token = default)
    {
        try
        {
            var response = await service.GetByIdAsync(id, token);
            return new TemplateGetResponse()
            {
                Id = response.Id,
                Name = response.Name,
                Type = response.Type,
                Content = response.Content,
                DateCreation = response.DateCreation
            };
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Data);
        }
    }

    [HttpGet]
    public IActionResult Download([FromBody] TemplateGetResponse file)
    {
        if (file.Content == null)
            return NotFound();

        return File(file.Content, file.ContentType, file.Name);
    }
}

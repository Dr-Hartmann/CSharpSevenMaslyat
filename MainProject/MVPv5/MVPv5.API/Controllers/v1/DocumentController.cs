using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using MVPv5.Application.Contracts.Document.v1;
using MVPv5.Domain.Abstractions.v1;
using MVPv5.Domain.Models;

namespace MVPv5.API.Controllers.v1;

[ApiController]
[Area("v1")]
[Route("[controller]")]
public class DocumentController(IDocumentService service, ITemplateService templateService/*, UserController userController*/) : ControllerBase
{
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Add([FromBody] DocumentCreateRequest request, CancellationToken token)
    {
        //var user = (await userController.Get(token)).Value;
        //var template = (await templateController.Get(request.TemplateId, token)).Value;
        //if (user is null || template is null)
        //{
        //    return NotFound();
        //}
        //await service.CreateAsync(DocumentModel.Create(request.Name, null, request.Data, template.Id, user.Id), token);
        await service.CreateAsync(DocumentModel.Create(request.Name, null, request.Data, request.TemplateId, request.UserId), token);
        return Created();
    }

    [HttpGet("read/{id:int}")]
    [ProducesResponseType(typeof(DocumentReadResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<DocumentReadResponse>> GetById(int id, CancellationToken token)
    {
        var document = await service.GetByIdAsync(id, token);
        return Ok(ModelToResponse(document));
    }

    [HttpGet("read-all")]
    [ProducesResponseType(typeof(IEnumerable<DocumentReadResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DocumentReadResponse>>> GetAll(CancellationToken token)
    {
        var documents = await service.GetAllAsync(token);
        return Ok(documents.Select(m => new DocumentReadResponse(
            m.Id,
            m.Name,
            m.DateCreation,
            m.Dictionary,
            m.TemplateId,
            m.UserId
        )));
    }

    //[HttpPut("update")]
    //[ProducesResponseType(StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> Update([FromBody] DocumentUpdateRequest request, CancellationToken token)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return ValidationProblem(ModelState);
    //    }
    //    await service.UpdateAsync(request.Id, DocumentModel.Create(
    //            request.Name,
    //            null,
    //            request.Data,
    //            request.TemplateId,
    //            request.UserId),
    //        token);
    //    return Created();
    //}

    [HttpPatch("update")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] DocumentPatchRequest request, CancellationToken token)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            await service.UpdateNameAsync(request.Id, request.Name, token);
        }
        if (request.Data is not null)
        {
            await service.UpdateMetaDataById(request.Id, request.Data, token);
        }

        return Ok();
    }

    //TODO...

    [HttpDelete("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteById(int id, CancellationToken token)
    {
        await service.DeleteByIdAsync(id, token);
        return NoContent();
    }

    private Stream DocumentEditor(byte[] content, IDictionary<string, string> Data)
    {
        // TODO - поменять немного логику
        using var outputStream = new MemoryStream();
        outputStream.Write(content, 0, content.Length);
        outputStream.Position = 0;

        using var doc = WordprocessingDocument.Open(outputStream, true);
        var body = doc.MainDocumentPart?.Document.Body;

        foreach (var text in body?.Descendants<Text>() ?? Enumerable.Empty<Text>())
        {
            foreach (var replacement in Data)
            {
                if (!text.InnerText.Contains(replacement.Key)) continue;

                if (replacement.Value.Contains("\r\n"))
                {
                    if (text.Parent is not Run run || run.Parent is not Paragraph paragraph) continue;

                    var pPr = paragraph.ParagraphProperties;
                    var rPr = run.RunProperties;
                    int index = body!.ToList().IndexOf(paragraph);
                    paragraph.Remove();

                    foreach (var newText in replacement.Value.Split("\r\n", StringSplitOptions.None))
                    {
                        var newPar = new Paragraph();
                        if (pPr != null) newPar.ParagraphProperties = (ParagraphProperties)pPr.CloneNode(true);

                        var newRun = new Run();
                        if (rPr != null) newRun.RunProperties = (RunProperties)rPr.CloneNode(true);

                        newRun.Append(new Text(newText));
                        newPar.Append(newRun);
                        body!.InsertAt(newPar, index++);
                    }
                }
                else
                {
                    text.Text = text.Text.Replace(replacement.Key, replacement.Value);
                }
            }

            doc.Save();
        }

        var result = new MemoryStream(outputStream.ToArray());
        result.Position = 0;
        return result;
    }

    [HttpGet("build-and-download/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DownloadByID(int id, CancellationToken token)
    {
        var response = await service.GetByIdAsync(id, token);
        if (response is null)
        {
            return NotFound("Документ не найден");
        }

        var template = await templateService.GetByIdAsync(response.TemplateId, token);
        if (template is null)
        {
            return NotFound("Шаблон не найден");
        }

        if (template.Content is null)
        {
            return NotFound("Файл шаблона не найден");
        }

        var stream = DocumentEditor(template.Content, response.Dictionary ?? new Dictionary<string, string>());

        return File(stream, template.ContentType ?? "", response.Name, true);
    }

    private static DocumentReadResponse ModelToResponse(DocumentModel m) => new(
        m.Id,
        m.Name,
        m.DateCreation,
        m.Dictionary,
        m.TemplateId,
        m.UserId
    );
}

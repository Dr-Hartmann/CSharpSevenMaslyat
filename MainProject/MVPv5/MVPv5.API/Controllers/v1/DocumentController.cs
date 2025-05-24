using Azure.Core;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVPv5.Application.Contracts.Document.v1;
using MVPv5.Core.Abstractions.v1;
using MVPv5.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MVPv5.API.Controllers.v1;

[ApiController]
[Area("v1")]
[Route("[controller]")]
public class DocumentController(IDocumentService service/*, ILogger<DocumentController> logger*/) : ControllerBase
{
    private static DocumentReadResponse ModelToResponse(DocumentModel m) =>
        new(
            m.Id,
            m.Name,
            m.DateCreation,
            m.MetadataJson?.RootElement.ToString() ?? "{}",
            m.TemplateId,
            m.UserId
        );

    /// <summary>
    /// Строит документ на основе шаблона и переданных данных.
    /// </summary>
    /// <returns>Сформированный документ в виде массива байт.</returns>
    /// <response code="200">Документ успешно построен.</response>
    [HttpPost("build-document")]
    [ProducesResponseType(typeof(DocumentBuildResponse), StatusCodes.Status200OK)]
    public ActionResult<DocumentBuildResponse> DocumentEditor([FromBody] DocumentBuildRequest request)
    {
        using var stream = new MemoryStream(request.Content);

        using var doc = WordprocessingDocument.Open(stream, true);
        var body = doc.MainDocumentPart?.Document.Body;

        foreach (var text in body?.Descendants<Text>() ?? Enumerable.Empty<Text>())
        {
            foreach (var replacement in request.Data)
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
                        body.InsertAt(newPar, index++);
                    }
                }
                else
                {
                    text.Text = text.Text.Replace(replacement.Key, replacement.Value);
                }
            }
        }

        doc.Save();

        return Ok(new DocumentBuildResponse(request.Name, stream.ToArray()));
    }


    /// <summary>
    /// Создает новый документ.
    /// </summary>
    /// <returns>HTTP 201 при успешном создании.</returns>
    /// <response code="201">Документ успешно создан.</response>
    /// <response code="400">Ошибка валидации входных данных.</response>
    /// <response code="409">Шаблон с таким именем уже существует.</response>
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //[Authorize]
    public async Task<IActionResult> Add([FromBody] DocumentCreateRequest request, CancellationToken token)
    {
        var metadataJson = JsonDocument.Parse(request.MetadataJson);

        await service.CreateAsync(
            request.Name,
            request.DateCreation,
            metadataJson,
            request.TemplateId,
            request.UserId,
            token
        );

        return Created();
    }

    /// <summary>
    /// Экспортирует документ в формате JSON.
    /// </summary>
    /// <param name="id">Идентификатор документа.</param>
    /// <returns>Файл JSON с данными документа.</returns>
    /// <response code="200">Документ экспортирован в виде JSON-файла.</response>
    [HttpGet("export/{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> ExportDocument(int id, CancellationToken token)
    {
        var doc = await service.GetByIdAsync(id, token);

        var json = JsonSerializer.Serialize(doc, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        var fileName = $"document_{id}.json";
        var fileBytes = System.Text.Encoding.UTF8.GetBytes(json);

        return File(fileBytes, "application/json", fileName);
    }

    /// <summary>
    /// Импортирует документ из JSON.
    /// </summary>
    /// <returns>HTTP 201 при успешной загрузке.</returns>
    /// <response code="201">Документ успешно импортирован.</response>
    /// <response code="400">Ошибка десериализации.</response>
    [HttpPost("import")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ImportDocument([FromBody] JsonElement json, CancellationToken token)
    {
        var doc = JsonSerializer.Deserialize<DocumentModel>(json.GetRawText())
                  ?? throw new ValidationException("Невозможно десериализовать документ");

        await service.CreateAsync(
            doc.Name,
            doc.DateCreation,
            doc.MetadataJson ?? JsonDocument.Parse("{}"),
            doc.TemplateId,
            doc.UserId,
            token
        );

        return Created();
    }

    /// <summary>
    /// Получает документ по его идентификатору.
    /// </summary>
    /// <returns>Документ, если найден.</returns>
    /// <response code="200">Документ найден.</response>
    [HttpGet("read/{id:int}")]
    [ProducesResponseType(typeof(DocumentReadResponse), StatusCodes.Status200OK)]
    //[Authorize]
    public async Task<ActionResult<DocumentReadResponse>> GetById(int id, CancellationToken token)
    {
        var document = await service.GetByIdAsync(id, token);
        return Ok(ModelToResponse(document));
    }

    /// <summary>
    /// Получает список всех документов.
    /// </summary>
    /// <returns>Список документов.</returns>
    /// <response code="200">Список получен успешно.</response>
    [HttpGet("read-all")]
    [ProducesResponseType(typeof(IEnumerable<DocumentReadResponse>), StatusCodes.Status200OK)]
    //[Authorize]
    public async Task<ActionResult<IEnumerable<DocumentReadResponse>>> GetAll(CancellationToken token)
    {
        var documents = await service.GetAllAsync(token);
        return Ok(documents.Select(ModelToResponse));
    }

    /// <summary>
    /// Полностью обновляет документ.
    /// </summary>
    /// <returns>HTTP 201 при успешном обновлении.</returns>
    /// <response code="201">Документ успешно обновлен.</response>
    /// <response code="400">Ошибка валидации входных данных.</response>
    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[Authorize]
    public async Task<IActionResult> Update([FromBody] DocumentUpdateRequest request, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var metadataJson = JsonDocument.Parse(request.MetadataJson);

        await service.UpdateAsync(
            request.Id,
            request.Name,
            request.DateCreation,
            metadataJson,
            request.TemplateId,
            request.UserId,
            token
        );

        return Created();
    }

    /// <summary>
    /// Частично обновляет имя документа.
    /// </summary>
    /// <returns>HTTP 201 при успешном обновлении.</returns>
    /// <response code="201">Имя успешно обновлено.</response>
    /// <response code="400">Ошибка валидации входных данных.</response>
    [HttpPatch("updateName")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[Authorize]
    public async Task<IActionResult> PatchName([FromBody] DocumentPatchNameRequest request, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        await service.UpdateNameAsync(request.Id, request.Name, token);
        return Created();
    }


    /// <summary>
    /// Частично обновляет метаданные документа.
    /// </summary>
    /// <returns>HTTP 201 при успешном обновлении.</returns>
    /// <response code="201">Метаданные успешно обновлены.</response>
    /// <response code="400">Ошибка валидации входных данных.</response>
    [HttpPatch("updateMetadata")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[Authorize]
    public async Task<IActionResult> PatchMetadata([FromBody] DocumentPatchMetadataRequest request, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var metadataJson = JsonDocument.Parse(request.MetadataJson);
        await service.UpdateMetaDataById(request.Id, metadataJson, token);
        return Created();
    }

    /// <summary>
    /// Удаляет документ по данным из тела запроса.
    /// </summary>
    /// <returns>HTTP 204 если успешно.</returns>
    /// <response code="204">Документ успешно удален.</response>
    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //[Authorize]
    public async Task<IActionResult> Delete([FromBody] DocumentDeleteRequest request, CancellationToken token)
    {
        await service.DeleteByIdAsync(request.Id, token);
        return NoContent();
    }

    /// <summary>
    /// Удаляет документ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор документа.</param>
    /// <returns>HTTP 204 если успешно.</returns>
    /// <response code="204">Документ успешно удален.</response>
    [HttpDelete("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //[Authorize]
    public async Task<IActionResult> DeleteById(int id, CancellationToken token)
    {
        await service.DeleteByIdAsync(id, token);
        return NoContent();
    }
}
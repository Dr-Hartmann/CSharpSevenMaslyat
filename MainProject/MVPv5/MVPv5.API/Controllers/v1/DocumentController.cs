using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVPv5.Application.Contracts.Document.v1;

namespace MVPv5.API.Controllers.v1;

[ApiController]
[Area("v1")]
[Route("[controller]")]
public class DocumentController(/*IDocumentService service*/ILogger<DocumentController> logger/*, AuditService auditService*/) : ControllerBase
{
    [HttpPost("build-document")]
    public ActionResult<DocumentBuildResponse> DocumentEditor([FromBody] DocumentBuildRequest request)
    {
        try
        {
            using var stream = new MemoryStream();
            stream.Write(request.Content, 0, request.Content.Length);

            using var doc = WordprocessingDocument.Open(stream, true);
            var body = doc.MainDocumentPart!.Document.Body;

            foreach (var text in body!.Descendants<Text>())
            {
                foreach (var replacement in request.Data)
                {
                    if (!text.InnerText.Contains(replacement.Key)) continue;
                    if (replacement.Value.Contains("\r\n"))
                    {
                        var run = (Run)text.Parent;
                        if (run == null) continue;
                        var paragraph = (Paragraph)run.Parent;
                        if (paragraph == null) continue;

                        var pPr = paragraph.ParagraphProperties;
                        var rPr = run.RunProperties;
                        int index = body.ToList().IndexOf(paragraph);
                        paragraph.Remove();

                        string[] newParagraphs
                            = replacement.Value.Split("\r\n", StringSplitOptions.None);

                        foreach (var newText in newParagraphs)
                        {
                            Paragraph newPar = new();
                            if (pPr != null)
                            {
                                newPar.ParagraphProperties =
                                    (ParagraphProperties)pPr.CloneNode(true);
                            }

                            Run newRun = new();
                            if (rPr != null)
                            {
                                newRun.RunProperties =
                                    (RunProperties)rPr.CloneNode(true);
                            }

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
                doc.Save();
            }

            return Ok(new DocumentBuildResponse(request.Name, stream.ToArray()));
        }
        catch(Exception ex)
        {
            logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }


    //[HttpPost]
    //[Authorize]
    ////[ValidateAntiForgeryToken]



    //[HttpDelete("{id}")]
    //[Authorize]
    //public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken)
    //{
    //    try
    //    {
    //        // Аудит удаления документа
    //        await auditService.AuditAsync($"Удаление документа: {id}", User?.Identity?.Name ?? "anonymous", HttpContext.RequestAborted);
    //        await docEditorService.DeleteAsync(id, cancellationToken);
    //        return Ok();
    //    }
    //    catch (KeyNotFoundException)
    //    {
    //        return NotFound();
    //    }
    //}
}







//[ApiController]
//[Route("[controller]/[action]")]
//public class DocumentEditorController(DocumentEditorService documentService) : ControllerBase
//{
//    // Экспорт документа в JSON-файл
//    [HttpGet("export/{id}")]
//    public IActionResult ExportDocument(int id)
//    {
//        var doc = documentService.LoadDocument(id);
//        var json = JsonHelper.Serialize(doc);
//        return File(System.Text.Encoding.UTF8.GetBytes(json), "application/json", $"document_{id}.json");
//    }

//    // Импорт документа из JSON-строки
//    [HttpPost("import")]
//    public IActionResult ImportDocument([FromBody] string json)
//    {
//        var doc = JsonHelper.Deserialize<DocumentModel>(json);
//        documentService.SaveDocument(doc);
//        return Ok();
//    }
//}
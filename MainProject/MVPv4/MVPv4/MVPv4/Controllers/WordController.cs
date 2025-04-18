//using Microsoft.AspNetCore.Mvc;
//using MVPv4.Client.Services;

//namespace MVPv4.Components.Controllers;

//[ApiController]
//[Route("api/[controller]")]
//public class WordController : Controller
//{
//    private readonly WordDocumentService _wordService;

//    public WordController(WordDocumentService wordService)
//    {
//        _wordService = wordService;
//    }

//    [HttpPost("read")]
//    public async Task<ActionResult<string>> ReadWordDocument([FromForm] IFormFile file)
//    {
//        using var memoryStream = new MemoryStream();
//        await file.CopyToAsync(memoryStream);
//        var text = _wordService.ReadWordDocument(memoryStream.ToArray());
//        return Ok(text);
//    }
//}

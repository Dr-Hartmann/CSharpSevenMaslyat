using DTOmvp;
using Microsoft.AspNetCore.Mvc;

namespace MVPv4.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class DocumentEditorController : Controller
{
    private readonly IDocumentEditorService docEditorService;

    public DocumentEditorController(IDocumentEditorService service)
    {
        docEditorService = service;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DTOdocumentV1>> Get(int? id)
    {
        try
        {
            var doc = await docEditorService.GetDocumentFromDatabase(id);
            return Ok(doc);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DTOdocumentV1>>> All()
    {
        try
        {
            var doc = await docEditorService.GetAll();
            return Ok(doc);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Create(IFormCollection collection)
    //{
    //    docEditorService.
    //}
}

//[ApiController]
//[Route("test5/[controller]")]
//public class TestController() : Controller
//{
//    private readonly List<DTOdocumentV1> _products = new()
//    {
//        new DTOdocumentV1 { Id = 1, Title = "Laptop" },
//        new DTOdocumentV1 { Id = 2, Title = "Smartphone" }
//    };

//    [HttpGet]
//    public async Task<ActionResult<IEnumerable<DTOdocumentV1>>> Get()
//    {
//        await Task.Delay(1000);
//        return Ok(_products); // Возвращает JSON с кодом 200
//    }

//    [HttpGet("{id}")]
//    public async Task<ActionResult<DTOdocumentV1>> GetProductById(int? id)
//    {
//        await Task.Delay(1000);

//        var product = _products.FirstOrDefault(p => p.Id == id);

//        if (product == null)
//        {
//            return NotFound(); // 404 если не найден
//        }

//        return Ok(product); // 200 + данные продукта
//    }

//    [HttpPost]
//    public IActionResult HandleTest([FromBody] string a)
//    {
//        return Ok();
//    }
//}

using WebAPI_ASPNET_Core;
using Microsoft.AspNetCore.Mvc;
using DTOmvp;

namespace WebAPI_ASPNET_Core.Controllers;

/// <summary>
/// API-контроллер для операций редактирования документов
/// Обрабатывает получение и управление документами
/// </summary>
[ApiController] // Указывает, что этот класс является API-контроллером
[Route("[controller]/[action]")] // Определяет шаблон маршрута для всех действий контроллера
public class DocumentEditorController : Controller
{
    // Сервис для операций редактирования документов
    // private readonly означает, что поле может быть установлено только в конструкторе
    private readonly IDocumentEditorService docEditorService;

    // Конструктор, который внедряет сервис редактора документов
    // Внедрение зависимостей автоматически предоставляет экземпляр сервиса
    public DocumentEditorController(IDocumentEditorService service)
    {
        docEditorService = service;
    }

    /// <summary>
    /// Получает один документ по его ID
    /// </summary>
    /// <param name="id">ID документа для получения</param>
    /// <returns>Данные документа, если найден, NotFound если документ не существует</returns>
    [HttpGet("{id}")] // Указывает, что этот метод обрабатывает HTTP GET запросы с параметром id
    public async Task<ActionResult<DTOdocumentV1>> Get(int? id)
    {
        try
        {
            // Попытка получить документ из базы данных
            // await приостанавливает выполнение метода до завершения асинхронной операции
            var doc = await docEditorService.GetDocumentFromDatabase(id);
            // Ok() возвращает HTTP 200 OK с данными документа
            return Ok(doc);
        }
        catch (KeyNotFoundException)
        {
            // Возвращает 404, если документ не найден
            // NotFound() возвращает HTTP 404 Not Found
            return NotFound();
        }
    }

    /// <summary>
    /// Получает все документы из базы данных
    /// </summary>
    /// <returns>Список всех документов при успехе, NotFound если документов не существует</returns>
    [HttpGet] // Указывает, что этот метод обрабатывает HTTP GET запросы без параметров
    public async Task<ActionResult<IEnumerable<DTOdocumentV1>>> All()
    {
        try
        {
            // Получает все документы из сервиса
            // IEnumerable<DTOdocumentV1> представляет коллекцию документов
            var doc = await docEditorService.GetAll();
            // Ok() возвращает HTTP 200 OK с коллекцией документов
            return Ok(doc);
        }
        catch (KeyNotFoundException)
        {
            // Возвращает 404, если документы не найдены
            return NotFound();
        }
    }

    // Закомментированный эндпоинт создания с проверкой токена защиты от подделки
    //[HttpPost] // Указывает, что этот метод обрабатывает HTTP POST запросы
    //[ValidateAntiForgeryToken] // Проверяет токен защиты от подделки
    //public ActionResult Create(IFormCollection collection)
    //{
    //    docEditorService.
    //}
}

// Закомментированный тестовый контроллер с примерами эндпоинтов
//[ApiController]
//[Route("test5/[controller]")]
//public class TestController() : Controller
//{
//    // Инициализирует список тестовых продуктов
//    // new() - синтаксис целевого типа, компилятор выводит тип из контекста
//    private readonly List<DTOdocumentV1> _products = new()
//    {
//        new DTOdocumentV1 { Id = 1, Title = "Laptop" },
//        new DTOdocumentV1 { Id = 2, Title = "Smartphone" }
//    };

//    [HttpGet]
//    public async Task<ActionResult<IEnumerable<DTOdocumentV1>>> Get()
//    {
//        // Имитирует задержку в 1 секунду
//        await Task.Delay(1000);
//        // Возвращает JSON с кодом 200
//        return Ok(_products);
//    }

//    [HttpGet("{id}")]
//    public async Task<ActionResult<DTOdocumentV1>> GetProductById(int? id)
//    {
//        // Имитирует задержку в 1 секунду
//        await Task.Delay(1000);

//        // Ищет продукт по ID
//        // FirstOrDefault возвращает первый элемент, удовлетворяющий условию, или null
//        var product = _products.FirstOrDefault(p => p.Id == id);

//        if (product == null)
//        {
//            // 404 если не найден
//            return NotFound();
//        }

//        // 200 + данные продукта
//        return Ok(product);
//    }

//    [HttpPost]
//    public IActionResult HandleTest([FromBody] string a)
//    {
//        // Возвращает HTTP 200 OK без данных
//        return Ok();
//    }
//}

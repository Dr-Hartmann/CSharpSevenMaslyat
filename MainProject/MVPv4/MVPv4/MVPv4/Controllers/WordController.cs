//using Microsoft.AspNetCore.Mvc;
//using MVPv4.Client.Services;

//namespace MVPv4.Components.Controllers;

/// <summary>
/// API-контроллер для операций с документами Word
/// Обрабатывает чтение и обработку документов Word
/// </summary>
//[ApiController] // Указывает, что этот класс является API-контроллером
//[Route("api/[controller]")] // Определяет шаблон маршрута с префиксом "api"
//public class WordController : Controller
//{
//    // Сервис для операций с документами Word
//    // private readonly означает, что поле может быть установлено только в конструкторе
//    private readonly WordDocumentService _wordService;

//    // Конструктор, который внедряет сервис документов Word
//    // Внедрение зависимостей автоматически предоставляет экземпляр сервиса
//    public WordController(WordDocumentService wordService)
//    {
//        _wordService = wordService;
//    }

//    /// <summary>
//    /// Читает содержимое из файла документа Word
//    /// </summary>
//    /// <param name="file">Файл документа Word для чтения</param>
//    /// <returns>Текстовое содержимое документа</returns>
//    [HttpPost("read")] // Указывает, что этот метод обрабатывает HTTP POST запросы по пути "read"
//    public async Task<ActionResult<string>> ReadWordDocument([FromForm] IFormFile file)
//    {
//        // Создает поток в памяти для хранения содержимого файла
//        // using гарантирует освобождение ресурсов после использования
//        using var memoryStream = new MemoryStream();
        
//        // Копирует содержимое файла в поток в памяти
//        // await приостанавливает выполнение метода до завершения асинхронной операции
//        await file.CopyToAsync(memoryStream);
        
//        // Читает содержимое документа с помощью сервиса
//        // ToArray() преобразует поток в массив байтов
//        var text = _wordService.ReadWordDocument(memoryStream.ToArray());
        
//        // Возвращает текстовое содержимое
//        // Ok() возвращает HTTP 200 OK с текстом документа
//        return Ok(text);
//    }
//}

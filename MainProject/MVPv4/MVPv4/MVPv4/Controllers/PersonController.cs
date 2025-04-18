using DTOmvp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVPv4.Client;

/// <summary>
/// API-контроллер для операций управления персонами
/// Обрабатывает CRUD-операции для сущностей персон
/// </summary>
[ApiController] // Указывает, что этот класс является API-контроллером
[Route("input/[controller]/[action]")] // Определяет шаблон маршрута с префиксом "input"
public class PersonController : Controller
{
    // Репозиторий для операций с данными персон
    // private readonly означает, что поле может быть установлено только в конструкторе
    private readonly PersonRepository personRepository;

    // Конструктор, который внедряет репозиторий персон
    // Внедрение зависимостей автоматически предоставляет экземпляр репозитория
    public PersonController(PersonRepository personRepository)
    {
        this.personRepository = personRepository;
    }

    /// <summary>
    /// Получает всех персон из репозитория
    /// </summary>
    /// <returns>Коллекция всех персон</returns>
    [HttpGet] // Указывает, что этот метод обрабатывает HTTP GET запросы
    public IEnumerable<Person> GetAllPersons()
    {
        // Возвращает всех персон из репозитория
        // IEnumerable<Person> представляет коллекцию персон
        return personRepository.GetAll();
    }

    /// <summary>
    /// Добавляет новую персону в репозиторий
    /// </summary>
    /// <param name="person">Объект персоны для добавления</param>
    [HttpPost] // Указывает, что этот метод обрабатывает HTTP POST запросы
    public void Post(Person? person)
    {
        // Добавляет персону в репозиторий
        // Метод Add репозитория сохраняет новую персону в хранилище данных
        personRepository.Add(person);
    }
}

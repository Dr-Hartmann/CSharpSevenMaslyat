using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetCoreMVC.Data;
using AspNetCoreMVC.Models;

namespace AspNetCoreMVC.Controllers;

/// <summary>
/// Контроллер для управления фильмами в базе данных.
/// Реализует стандартные CRUD операции (Create, Read, Update, Delete).
/// </summary>
public class MoviesController : Controller
{
    // Поле для хранения контекста базы данных
    // readonly означает, что значение можно присвоить только в конструкторе
    private readonly AspNetCoreMVCContext _context;

    // Конструктор контроллера
    // context - контекст базы данных, который автоматически внедряется через DI (Dependency Injection)
    public MoviesController(AspNetCoreMVCContext context)
    {
        // Сохраняем переданный контекст в поле класса
        _context = context;
    }

    /// <summary>
    /// GET метод для отображения списка фильмов с возможностью фильтрации
    /// </summary>
    /// <param name="movieGenre">Жанр для фильтрации</param>
    /// <param name="searchString">Строка поиска по названию</param>
    /// <returns>Представление со списком фильмов</returns>
    [HttpGet] // Атрибут, указывающий, что метод обрабатывает HTTP GET запросы
    public async Task<IActionResult> Index(string movieGenre, string searchString)
    {
        // Проверка наличия таблицы Movies в базе данных
        // Если таблица не существует, возвращаем ошибку
        if (_context.Movies == null)
        {
            return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
        }

        // Создаем LINQ-запрос для получения всех жанров из базы данных
        // IQueryable<string> - тип, представляющий запрос к базе данных
        // from m in _context.Movies - выбираем из таблицы Movies
        // orderby m.Genre - сортируем по полю Genre
        // select m.Genre - выбираем только поле Genre
        IQueryable<string> genreQuery = from m in _context.Movies
                                        orderby m.Genre
                                        select m.Genre;
        
        // Создаем LINQ-запрос для получения всех фильмов
        // var - автоматическое определение типа
        // from m in _context.Movies - выбираем из таблицы Movies
        // select m - выбираем все поля
        var movies = from m in _context.Movies
                     select m;

        // Если указана строка поиска, применяем фильтрацию
        // string.IsNullOrEmpty - проверяет, пустая ли строка или null
        if (!string.IsNullOrEmpty(searchString))
        {
            // Добавляем условие фильтрации в запрос
            // Where - метод LINQ для фильтрации
            // s.Title!.ToUpper() - преобразуем название в верхний регистр
            // Contains - проверяет, содержит ли строка подстроку
            movies = movies.Where(s => s.Title!.ToUpper().Contains(searchString.ToUpper()));
        }

        // Если указан жанр, применяем фильтрацию по жанру
        if (!string.IsNullOrEmpty(movieGenre))
        {
            // Добавляем условие фильтрации по жанру
            movies = movies.Where(x => x.Genre == movieGenre);
        }

        // Создаем модель представления
        // new MovieGenreViewModel - создаем новый экземпляр класса
        var movieGenreVM = new MovieGenreViewModel
        {
            // Создаем выпадающий список жанров
            // new SelectList - создает список для HTML select
            // await - ждет завершения асинхронной операции
            // genreQuery.Distinct() - убирает дубликаты
            // ToListAsync() - выполняет запрос и преобразует в список
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
            
            // Получаем список фильмов
            // await - ждет завершения асинхронной операции
            // movies.ToListAsync() - выполняет запрос и преобразует в список
            Movies = await movies.ToListAsync()
        };

        // Возвращаем представление с моделью
        // View(movieGenreVM) - создает представление с переданной моделью
        return View(movieGenreVM);
    }

    /// <summary>
    /// POST метод для обработки поискового запроса
    /// </summary>
    [HttpPost] // Атрибут, указывающий, что метод обрабатывает HTTP POST запросы
    public string Index(string searchString, bool notUsed)
    {
        // Возвращаем строку с результатом поиска
        return "From [HttpPost]Index: filter on " + searchString;
    }

    /// <summary>
    /// Отображение детальной информации о конкретном фильме
    /// </summary>
    /// <param name="id">ID фильма</param>
    public async Task<IActionResult> Details(int? id)
    {
        // Проверяем, передан ли ID
        // int? - nullable тип, может быть null
        if (id == null)
        {
            // Если ID не передан, возвращаем 404
            return NotFound();
        }

        // Ищем фильм в базе данных
        // await - ждет завершения асинхронной операции
        // _context.Movies - обращение к таблице Movies
        // FirstOrDefaultAsync - находит первый элемент или null
        // m => m.ID == id - лямбда-выражение для поиска по ID
        var movies = await _context.Movies
            .FirstOrDefaultAsync(m => m.ID == id);
            
        // Проверяем, найден ли фильм
        if (movies == null)
        {
            // Если фильм не найден, возвращаем 404
            return NotFound();
        }

        // Возвращаем представление с найденным фильмом
        return View(movies);
    }

    /// <summary>
    /// Отображение формы для создания нового фильма
    /// </summary>
    [HttpGet]
    public IActionResult Create()
    {
        // Возвращаем пустое представление для создания нового фильма
        return View();
    }

    /// <summary>
    /// Обработка данных формы создания нового фильма
    /// </summary>
    /// <param name="movies">Данные нового фильма</param>
    [HttpPost]
    [ValidateAntiForgeryToken] // Защита от CSRF атак
    public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,Genre,Price")] Movies movies)
    {
        // Проверяем валидность данных модели
        // ModelState.IsValid - проверяет все правила валидации
        if (ModelState.IsValid)
        {
            // Добавляем новый фильм в контекст
            _context.Add(movies);
            // Сохраняем изменения в базе данных
            await _context.SaveChangesAsync();
            // Перенаправляем на страницу со списком фильмов
            return RedirectToAction(nameof(Index));
        }
        // Если данные невалидны, возвращаем форму с ошибками
        return View(movies);
    }

    /// <summary>
    /// Отображение формы редактирования существующего фильма
    /// </summary>
    /// <param name="id">ID редактируемого фильма</param>
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        // Проверяем наличие ID
        if (id == null)
        {
            return NotFound();
        }

        // Ищем фильм по ID
        // FindAsync - ищет сущность по первичному ключу
        var movies = await _context.Movies.FindAsync(id);
        if (movies == null)
        {
            return NotFound();
        }
        // Возвращаем форму редактирования с данными фильма
        return View(movies);
    }

    /// <summary>
    /// Обработка данных формы редактирования фильма
    /// </summary>
    /// <param name="id">ID редактируемого фильма</param>
    /// <param name="movies">Обновленные данные фильма</param>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Movies movies)
    {
        // Проверяем соответствие ID в URL и в данных
        if (id != movies.ID)
        {
            return NotFound();
        }

        // Проверяем валидность данных
        if (ModelState.IsValid)
        {
            try
            {
                // Обновляем данные фильма в контексте
                _context.Update(movies);
                // Сохраняем изменения в базе данных
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Обрабатываем ситуацию, когда фильм был изменен или удален другим пользователем
                if (!MoviesExists(movies.ID))
                {
                    return NotFound();
                }
                else
                {
                    // Если произошла другая ошибка, пробрасываем её дальше
                    throw;
                }
            }
            // Перенаправляем на страницу со списком фильмов
            return RedirectToAction(nameof(Index));
        }
        // Если данные невалидны, возвращаем форму с ошибками
        return View(movies);
    }

    /// <summary>
    /// Отображение формы подтверждения удаления фильма
    /// </summary>
    /// <param name="id">ID удаляемого фильма</param>
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        // Проверяем наличие ID
        if (id == null)
        {
            return NotFound();
        }

        // Ищем фильм по ID
        var movies = await _context.Movies
            .FirstOrDefaultAsync(m => m.ID == id);
        if (movies == null)
        {
            return NotFound();
        }

        // Возвращаем форму подтверждения удаления с данными фильма
        return View(movies);
    }

    /// <summary>
    /// Обработка подтверждения удаления фильма
    /// </summary>
    /// <param name="id">ID удаляемого фильма</param>
    [HttpPost, ActionName("Delete")] // Указываем, что метод обрабатывает POST запрос с action="Delete"
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        // Проверяем наличие таблицы Movies
        if (_context.Movies == null)
        {
            return Problem("Entity set 'AspNetCoreMVCContext.Movies'  is null.");
        }

        // Ищем фильм по ID
        var movies = await _context.Movies.FindAsync(id);
        if (movies != null)
        {
            // Удаляем фильм из контекста
            _context.Movies.Remove(movies);
            // Сохраняем изменения в базе данных
            await _context.SaveChangesAsync();
        }

        // Перенаправляем на страницу со списком фильмов
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Проверяет существование фильма с указанным ID
    /// </summary>
    /// <param name="id">ID фильма для проверки</param>
    /// <returns>true, если фильм существует, иначе false</returns>
    private bool MoviesExists(int id)
    {
        // Проверяем наличие таблицы Movies
        // Any - проверяет, есть ли хотя бы один элемент, удовлетворяющий условию
        return (_context.Movies?.Any(e => e.ID == id)).GetValueOrDefault();
    }
}
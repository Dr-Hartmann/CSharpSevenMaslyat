using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AspNetCoreMVC.DTO;
using AspNetCoreMVC.Services;
using AspNetCoreMVC.Models;

namespace AspNetCoreMVC.Controllers;

/// <summary>
/// Контроллер для управления фильмами.
/// Использует сервисный слой для работы с данными.
/// </summary>
public class MoviesController : Controller
{
    private readonly IMovieService _movieService;

    /// <summary>
    /// Конструктор контроллера
    /// </summary>
    /// <param name="movieService">Сервис для работы с фильмами</param>
    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
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
        // Получаем список жанров для выпадающего списка
        var genres = await _movieService.GetGenresAsync();
        
        // Получаем отфильтрованные фильмы
        var movies = await _movieService.SearchMoviesAsync(movieGenre, searchString);

        // Создаем модель представления
        var movieGenreVM = new MovieGenreViewModel
        {
            Genres = new SelectList(genres),
            Movies = movies.Select(m => new Movies
            {
                ID = m.Id,
                Title = m.Title,
                ReleaseDate = m.ReleaseDate,
                Genre = m.Genre,
                Price = m.Price
            }).ToList(),
            MovieGenre = movieGenre,
            SearchString = searchString
        };

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
        if (id == null)
        {
            return NotFound();
        }

        var movieDto = await _movieService.GetMovieByIdAsync(id.Value);
        if (movieDto == null)
        {
            return NotFound();
        }

        var movie = new Movies
        {
            ID = movieDto.Id,
            Title = movieDto.Title,
            ReleaseDate = movieDto.ReleaseDate,
            Genre = movieDto.Genre,
            Price = movieDto.Price
        };

        return View(movie);
    }

    /// <summary>
    /// Отображение формы для создания нового фильма
    /// </summary>
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    /// <summary>
    /// Обработка данных формы создания нового фильма
    /// </summary>
    /// <param name="movie">Данные нового фильма</param>
    [HttpPost]
    [ValidateAntiForgeryToken] // Защита от CSRF атак
    public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,Genre,Price")] Movies movie)
    {
        if (ModelState.IsValid)
        {
            var movieDto = new MovieDto
            {
                Id = movie.ID,
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                Genre = movie.Genre,
                Price = movie.Price
            };

            await _movieService.CreateMovieAsync(movieDto);
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    /// <summary>
    /// Отображение формы редактирования существующего фильма
    /// </summary>
    /// <param name="id">ID редактируемого фильма</param>
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movieDto = await _movieService.GetMovieByIdAsync(id.Value);
        if (movieDto == null)
        {
            return NotFound();
        }

        var movie = new Movies
        {
            ID = movieDto.Id,
            Title = movieDto.Title,
            ReleaseDate = movieDto.ReleaseDate,
            Genre = movieDto.Genre,
            Price = movieDto.Price
        };

        return View(movie);
    }

    /// <summary>
    /// Обработка данных формы редактирования фильма
    /// </summary>
    /// <param name="id">ID редактируемого фильма</param>
    /// <param name="movie">Обновленные данные фильма</param>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,Title,ReleaseDate,Genre,Price")] Movies movie)
    {
        if (id != movie.ID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var movieDto = new MovieDto
                {
                    Id = movie.ID,
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    Genre = movie.Genre,
                    Price = movie.Price
                };

                await _movieService.UpdateMovieAsync(movieDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    /// <summary>
    /// Отображение формы подтверждения удаления фильма
    /// </summary>
    /// <param name="id">ID удаляемого фильма</param>
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movieDto = await _movieService.GetMovieByIdAsync(id.Value);
        if (movieDto == null)
        {
            return NotFound();
        }

        var movie = new Movies
        {
            ID = movieDto.Id,
            Title = movieDto.Title,
            ReleaseDate = movieDto.ReleaseDate,
            Genre = movieDto.Genre,
            Price = movieDto.Price
        };

        return View(movie);
    }

    /// <summary>
    /// Обработка подтверждения удаления фильма
    /// </summary>
    /// <param name="id">ID удаляемого фильма</param>
    [HttpPost, ActionName("Delete")] // Указываем, что метод обрабатывает POST запрос с action="Delete"
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _movieService.DeleteMovieAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
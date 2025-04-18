using AspNetCoreMVC.DTO;

namespace AspNetCoreMVC.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с фильмами.
    /// Определяет контракт между слоем представления и слоем доступа к данным.
    /// Позволяет:
    /// 1. Абстрагировать реализацию доступа к данным
    /// 2. Легко тестировать контроллеры
    /// 3. Изменять источник данных без изменения контроллеров
    /// </summary>
    public interface IMovieService
    {
        /// <summary>
        /// Получение списка всех фильмов
        /// </summary>
        /// <returns>Список DTO фильмов</returns>
        Task<List<MovieDto>> GetAllMoviesAsync();

        /// <summary>
        /// Получение фильма по ID
        /// </summary>
        /// <param name="id">ID фильма</param>
        /// <returns>DTO фильма или null, если не найден</returns>
        Task<MovieDto?> GetMovieByIdAsync(int id);

        /// <summary>
        /// Создание нового фильма
        /// </summary>
        /// <param name="movieDto">DTO нового фильма</param>
        /// <returns>Созданный DTO фильма</returns>
        Task<MovieDto> CreateMovieAsync(MovieDto movieDto);

        /// <summary>
        /// Обновление существующего фильма
        /// </summary>
        /// <param name="movieDto">DTO с обновленными данными</param>
        /// <returns>Обновленный DTO фильма</returns>
        Task<MovieDto> UpdateMovieAsync(MovieDto movieDto);

        /// <summary>
        /// Удаление фильма
        /// </summary>
        /// <param name="id">ID удаляемого фильма</param>
        /// <returns>true, если удаление успешно</returns>
        Task<bool> DeleteMovieAsync(int id);

        /// <summary>
        /// Получение списка уникальных жанров
        /// </summary>
        /// <returns>Список жанров</returns>
        Task<List<string>> GetGenresAsync();

        /// <summary>
        /// Поиск фильмов по жанру и названию
        /// </summary>
        /// <param name="genre">Жанр для фильтрации</param>
        /// <param name="searchString">Строка для поиска в названии</param>
        /// <returns>Список отфильтрованных DTO фильмов</returns>
        Task<List<MovieDto>> SearchMoviesAsync(string? genre, string? searchString);
    }
} 
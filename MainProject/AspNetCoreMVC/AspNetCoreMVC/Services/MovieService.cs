using AspNetCoreMVC.Data;
using AspNetCoreMVC.DTO;
using AspNetCoreMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreMVC.Services
{
    /// <summary>
    /// Реализация сервиса для работы с фильмами.
    /// Содержит бизнес-логику и преобразование между моделями и DTO.
    /// </summary>
    public class MovieService : IMovieService
    {
        private readonly AspNetCoreMVCContext _context;

        /// <summary>
        /// Конструктор сервиса
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public MovieService(AspNetCoreMVCContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение всех фильмов
        /// </summary>
        public async Task<List<MovieDto>> GetAllMoviesAsync()
        {
            return await _context.Movies
                .Select(m => MapToDto(m))
                .ToListAsync();
        }

        /// <summary>
        /// Получение фильма по ID
        /// </summary>
        public async Task<MovieDto?> GetMovieByIdAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            return movie == null ? null : MapToDto(movie);
        }

        /// <summary>
        /// Создание нового фильма
        /// </summary>
        public async Task<MovieDto> CreateMovieAsync(MovieDto movieDto)
        {
            var movie = MapToEntity(movieDto);
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return MapToDto(movie);
        }

        /// <summary>
        /// Обновление существующего фильма
        /// </summary>
        public async Task<MovieDto> UpdateMovieAsync(MovieDto movieDto)
        {
            var movie = await _context.Movies.FindAsync(movieDto.Id);
            if (movie == null)
                throw new KeyNotFoundException($"Movie with ID {movieDto.Id} not found");

            movie.Title = movieDto.Title;
            movie.ReleaseDate = movieDto.ReleaseDate;
            movie.Genre = movieDto.Genre;
            movie.Price = movieDto.Price;

            await _context.SaveChangesAsync();
            return MapToDto(movie);
        }

        /// <summary>
        /// Удаление фильма
        /// </summary>
        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return false;

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Получение списка жанров
        /// </summary>
        public async Task<List<string>> GetGenresAsync()
        {
            return await _context.Movies
                .Select(m => m.Genre)
                .Distinct()
                .ToListAsync();
        }

        /// <summary>
        /// Поиск фильмов по жанру и названию
        /// </summary>
        public async Task<List<MovieDto>> SearchMoviesAsync(string? genre, string? searchString)
        {
            var query = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(m => m.Title!.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(m => m.Genre == genre);
            }

            return await query
                .Select(m => MapToDto(m))
                .ToListAsync();
        }

        /// <summary>
        /// Преобразование сущности в DTO
        /// </summary>
        private static MovieDto MapToDto(Movies movie)
        {
            return new MovieDto
            {
                Id = movie.ID,
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                Genre = movie.Genre,
                Price = movie.Price
            };
        }

        /// <summary>
        /// Преобразование DTO в сущность
        /// </summary>
        private static Movies MapToEntity(MovieDto movieDto)
        {
            return new Movies
            {
                ID = movieDto.Id,
                Title = movieDto.Title,
                ReleaseDate = movieDto.ReleaseDate,
                Genre = movieDto.Genre,
                Price = movieDto.Price
            };
        }
    }
} 
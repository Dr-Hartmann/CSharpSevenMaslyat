using AspNetCoreMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AspNetCoreMVC.Models;

/// <summary>
/// Модель представления для страницы со списком фильмов.
/// Содержит данные, необходимые для отображения списка фильмов с возможностью фильтрации.
/// </summary>
public class MovieGenreViewModel
{
    /// <summary>
    /// Список фильмов для отображения.
    /// Оператор null! указывает компилятору, что это свойство не будет null,
    /// хотя оно инициализируется как null. Это нужно для подавления предупреждений компилятора.
    /// </summary>
    public List<Movies> Movies { get; set; } = null!;

    /// <summary>
    /// Список жанров для выпадающего списка фильтрации.
    /// SelectList - специальный тип в ASP.NET Core для работы с выпадающими списками.
    /// Содержит список значений и выбранное значение.
    /// </summary>
    public SelectList Genres { get; set; } = null!;

    /// <summary>
    /// Выбранный жанр для фильтрации.
    /// Пустая строка означает "все жанры".
    /// </summary>
    public string MovieGenre { get; set; } = string.Empty;

    /// <summary>
    /// Строка для поиска по названию фильма.
    /// Используется для фильтрации фильмов по названию.
    /// </summary>
    public string SearchString { get; set; } = string.Empty;
}
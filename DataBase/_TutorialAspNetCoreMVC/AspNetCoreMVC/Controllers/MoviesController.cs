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

public class MoviesController : Controller
{
    private readonly AspNetCoreMVCContext _context;

    public MoviesController(AspNetCoreMVCContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string movieGenre, string searchString)
    {
        if (_context.Movies == null)
        {
            return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
        }

        // Use LINQ to get list of genres.
        IQueryable<string> genreQuery = from m in _context.Movies
                                        orderby m.Genre
                                        select m.Genre;
        var movies = from m in _context.Movies
                     select m;

        if (!string.IsNullOrEmpty(searchString))
        {
            movies = movies.Where(s => s.Title!.ToUpper().Contains(searchString.ToUpper()));
        }

        if (!string.IsNullOrEmpty(movieGenre))
        {
            movies = movies.Where(x => x.Genre == movieGenre);
        }

        var movieGenreVM = new MovieGenreViewModel
        {
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
            Movies = await movies.ToListAsync()
        };

        return View(movieGenreVM);
    }

    [HttpPost]
    public string Index(string searchString, bool notUsed)
    {
        return "From [HttpPost]Index: filter on " + searchString;
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movies = await _context.Movies
            .FirstOrDefaultAsync(m => m.ID == id);
        if (movies == null)
        {
            return NotFound();
        }

        return View(movies);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,Genre,Price")] Movies movies)
    {
        if (ModelState.IsValid)
        {
            _context.Add(movies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(movies);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movies = await _context.Movies.FindAsync(id);
        if (movies == null)
        {
            return NotFound();
        }
        return View(movies);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Movies movies)
    {
        if (id != movies.ID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(movies);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoviesExists(movies.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(movies);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movies = await _context.Movies
            .FirstOrDefaultAsync(m => m.ID == id);
        if (movies == null)
        {
            return NotFound();
        }

        return View(movies);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var movies = await _context.Movies.FindAsync(id);
        if (movies != null)
        {
            _context.Movies.Remove(movies);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MoviesExists(int id)
    {
        return _context.Movies.Any(e => e.ID == id);
    }
}

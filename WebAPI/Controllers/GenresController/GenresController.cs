using Microsoft.AspNetCore.Mvc;
using DataLayer.Models;
using DataLayer.Services;

namespace WebAPI.Controllers.GenresController;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenresController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    // GET: api/Genres
    [HttpGet]
    public ActionResult<IEnumerable<Genre>> GetGenres()
    {
        return Ok(_genreService.GetAllGenres().ToList());
    }

    // GET: api/Genres/5
    [HttpGet("{id}")]
    public ActionResult<Genre> GetGenre(int id)
    {
        var genre = _genreService.GetGenresById(id).FirstOrDefault();
        if (genre == null)
        {
            return NotFound();
        }
        return Ok(genre);
    }

    // POST: api/Genres
    [HttpPost]
    public ActionResult<Genre> PostGenre(Genre genre)
    {
        _genreService.CreateGenre(genre);
        return CreatedAtAction(nameof(GetGenre), new { id = genre.GenreId }, genre);
    }

    // PUT: api/Genres/5
    [HttpPut("{id}")]
    public IActionResult PutGenre(int id, Genre genre)
    {
        if (id != genre.GenreId)
        {
            return BadRequest();
        }
        _genreService.UpdateGenre(genre);
        return NoContent();
    }

    // DELETE: api/Genres/5
    [HttpDelete("{id}")]
    public ActionResult<Genre> DeleteGenre(int id)
    {
        var success = _genreService.DeleteGenre(id);
        if (!success)
        {
            return NotFound();
        }
        return Ok();
    }
}

using DataLayer.Models;

namespace DataLayer.Services;

public interface IGenreService
{
    public IQueryable<Genre> GetAllGenres();
    public IQueryable<Genre> GetGenresById(int genreId);
    public IQueryable<VinylGenre> GetAllVinylGenres();
    public void CreateVinylGenre(int VinylId, int GenreId);
}
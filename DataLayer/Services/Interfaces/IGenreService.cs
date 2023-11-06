using DataLayer.Models;

namespace DataLayer.Services;

public interface IGenreService
{
    public IQueryable<Genre> GetAllGenres();
    public IQueryable<Genre> GetGenresById(int genreId);
    public IQueryable<VinylGenre> GetAllVinylGenres();
    public void CreateVinylGenre(int VinylId, int GenreId);
    void CreateGenre(Genre genre);
    void UpdateGenre(Genre genre);
    bool DeleteGenre(int genreId);

}
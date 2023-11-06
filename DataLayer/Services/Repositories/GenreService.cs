using DataLayer.Models;

namespace DataLayer.Services;

public class GenreService : IGenreService
{
    private readonly ApplicationDbContext _ApplicationDbContext;

    public GenreService(ApplicationDbContext melodyMineService)
    {
        _ApplicationDbContext = melodyMineService;
    }

    #region Genre
    public IQueryable<Genre> GetAllGenres()
    {
        IQueryable<Genre> tempGenres = _ApplicationDbContext.Genres.Distinct();
        
        return tempGenres;
    }

    
    public IQueryable<Genre> GetGenresById(int genreId)
    {
        IQueryable<Genre> tempGenres = _ApplicationDbContext.Genres.Where(c => c.GenreId == genreId);

        return tempGenres;
    }
    
    public IQueryable<VinylGenre> GetAllVinylGenres()
    {
        IQueryable<VinylGenre> tempGenres = _ApplicationDbContext.VinylGenres;
            
        return tempGenres;
    }
    
    public void CreateVinylGenre(int VinylId, int GenreId)
    {
        _ApplicationDbContext.VinylGenres.Add(new VinylGenre { VinylId = VinylId, GenreId = GenreId });
        _ApplicationDbContext.SaveChanges();
    }
    
    public void CreateGenre(Genre genre)
    {
        _ApplicationDbContext.Genres.Add(genre);
        _ApplicationDbContext.SaveChanges();
    }
    
    public void UpdateGenre(Genre genre)
    {
        var existingGenre = _ApplicationDbContext.Genres.Find(genre.GenreId);
        if (existingGenre != null)
        {
            existingGenre.GenreName = genre.GenreName;
            _ApplicationDbContext.SaveChanges();
        }
    }

    public bool DeleteGenre(int genreId)
    {
        var genre = _ApplicationDbContext.Genres.Find(genreId);
        if (genre != null)
        {
            _ApplicationDbContext.Genres.Remove(genre);
            _ApplicationDbContext.SaveChanges();
            return true;
        }
        return false;
    }
    #endregion
}
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
    #endregion
}
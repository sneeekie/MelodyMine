using DataLayer.Models;

namespace DataLayer.Services;

public class GenreService : IGenreService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public GenreService(ApplicationDbContext melodyMineService)
    {
        _applicationDbContext = melodyMineService;
    }

    #region Genre
    public IQueryable<Genre> GetAllGenres()
    {
        IQueryable<Genre> tempGenres = _applicationDbContext.Genres.Distinct();
        
        return tempGenres;
    }

    
    public IQueryable<Genre> GetGenresById(int genreId)
    {
        IQueryable<Genre> tempGenres = _applicationDbContext.Genres.Where(c => c.GenreId == genreId);

        return tempGenres;
    }
    
    public IQueryable<VinylGenre> GetAllVinylGenres()
    {
        IQueryable<VinylGenre> tempGenres = _applicationDbContext.VinylGenres;
            
        return tempGenres;
    }
    
    public void CreateVinylGenre(int VinylId, int GenreId)
    {
        _applicationDbContext.VinylGenres.Add(new VinylGenre { VinylId = VinylId, GenreId = GenreId });
        _applicationDbContext.SaveChanges();
    }
    
    public void CreateGenre(Genre genre)
    {
        _applicationDbContext.Genres.Add(genre);
        _applicationDbContext.SaveChanges();
    }
    
    public void UpdateGenre(Genre genre)
    {
        var existingGenre = _applicationDbContext.Genres.Find(genre.GenreId);
        if (existingGenre != null)
        {
            existingGenre.GenreName = genre.GenreName;
            _applicationDbContext.SaveChanges();
        }
    }

    public bool DeleteGenre(int genreId)
    {
        var genre = _applicationDbContext.Genres.Find(genreId);
        if (genre != null)
        {
            _applicationDbContext.Genres.Remove(genre);
            _applicationDbContext.SaveChanges();
            return true;
        }
        return false;
    }
    
    public async Task UpdateVinylGenreLink(int vinylId, int genreId)
    {
        var vinylGenre = _applicationDbContext.VinylGenres.FirstOrDefault(vg => vg.VinylId == vinylId);
        if (vinylGenre != null)
        {
            vinylGenre.GenreId = genreId;
            await _applicationDbContext.SaveChangesAsync();
        }
        else
        {
            _applicationDbContext.VinylGenres.Add(new VinylGenre { VinylId = vinylId, GenreId = genreId });
            await _applicationDbContext.SaveChangesAsync();
        }
    }
    #endregion
}
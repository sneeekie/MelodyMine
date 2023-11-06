using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Services;

public class VinylService : IVinylService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public VinylService(ApplicationDbContext context)
    {
        _applicationDbContext = context;
    }
    
    public void CreateVinyl(Vinyl vinyl)
    {
        _applicationDbContext.Vinyls.Add(vinyl);
        _applicationDbContext.SaveChanges();
    }

    public void DeleteVinylById(int vinylId)
    {
        var vinyl = _applicationDbContext.Vinyls.Find(vinylId);
        if (vinyl != null)
        {
            _applicationDbContext.Vinyls.Remove(vinyl);
            _applicationDbContext.SaveChanges();
        }
    }

    public Vinyl GetVinylById(int id)
    {
        return _applicationDbContext.Vinyls
            .Include(v => v.VinylGenres)
            .FirstOrDefault(v => v.VinylId == id);
    }

    public void UpdateVinylBy(int vinylId, Vinyl newVinyl)
    {
        var vinyl = _applicationDbContext.Vinyls.Find(vinylId);
        if (vinyl != null)
        {
            vinyl.Title = newVinyl.Title;
            vinyl.Artist = newVinyl.Artist;
            vinyl.Price = newVinyl.Price;
            vinyl.ImagePath = newVinyl.ImagePath;
            // Husk at opdatere de genrer, som vinylpladen tilhører, hvis det er nødvendigt.
            _applicationDbContext.SaveChanges();
        }
    }

    public IQueryable<Vinyl> GetAllVinyls()
    {
        return _applicationDbContext.Vinyls
            .Include(v => v.VinylGenres)
            .ThenInclude(vg => vg.Genre);
    }

    public IQueryable<Vinyl> GetAllVinylsPaged(int currentPage, int pageSize)
    {
        return GetAllVinyls()
            .OrderBy(v => v.VinylId)
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize);
    }
    
    
    //
    
    public IQueryable<Vinyl> GetAllFullVinyls()
    {
        // Antager at "full vinyls" refererer til vinylplader med deres genrer indlæst.
        return _applicationDbContext.Vinyls
            .Include(v => v.VinylGenres)
            .ThenInclude(vg => vg.Genre);
    }
    
    public IQueryable<Vinyl> GetAllFullVinylsPaged(int currentPage, int pageSize)
    {
        // Paginering anvendes på det fulde sæt af vinylplader.
        return GetAllFullVinyls()
            .OrderBy(v => v.VinylId)
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize);
    }
    
    public IQueryable<Vinyl> FilterVinylsPaged(int currentPage, int pageSize, string? searchTerm, int? genreId, string? filterTitle, string? price)
    {
        // Starter med at indlæse alle vinylplader med deres genrer.
        var query = GetAllFullVinyls();

        // Filtrering baseret på søgeterm.
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(v => v.Title.Contains(searchTerm));
        }

        // Filtrering baseret på genre.
        if (genreId.HasValue && genreId.Value > 0)
        {
            query = query.Where(v => v.VinylGenres.Any(vg => vg.GenreId == genreId.Value));
        }

        // Sortering baseret på titel eller pris.
        if (!string.IsNullOrWhiteSpace(filterTitle))
        {
            query = filterTitle == "+" 
                ? query.OrderBy(v => v.Title) 
                : query.OrderByDescending(v => v.Title);
        }

        if (!string.IsNullOrWhiteSpace(price))
        {
            query = price == "+" 
                ? query.OrderBy(v => v.Price) 
                : query.OrderByDescending(v => v.Price);
        }

        // Paginering anvendt til sidst.
        return query
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize);
    }
    
    public IQueryable<Vinyl> FilterVinyls(string? searchTerm, int? genreId, string? filterTitle, string? price)
    {
        // Start med at indlæse alle vinylplader med deres genrer.
        IQueryable<Vinyl> query = _applicationDbContext.Vinyls.Include(v => v.VinylGenres).ThenInclude(vg => vg.Genre);

        // Filtrering baseret på søgeterm.
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(v => v.Title.Contains(searchTerm));
        }

        // Filtrering baseret på genre.
        if (genreId.HasValue && genreId.Value > 0)
        {
            query = query.Where(v => v.VinylGenres.Any(vg => vg.GenreId == genreId.Value));
        }

        // Sortering baseret på titel eller pris.
        if (!string.IsNullOrWhiteSpace(filterTitle))
        {
            query = filterTitle == "+"
                ? query.OrderBy(v => v.Title)
                : query.OrderByDescending(v => v.Title);
        }

        if (!string.IsNullOrWhiteSpace(price))
        {
            query = price == "+"
                ? query.OrderBy(v => v.Price)
                : query.OrderByDescending(v => v.Price);
        }

        return query;
    }

}
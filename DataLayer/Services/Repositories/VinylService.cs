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
            vinyl.GenreId = newVinyl.GenreId;
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
        return _applicationDbContext.Vinyls
            .Include(v => v.VinylGenres)
            .ThenInclude(vg => vg.Genre);
    }
    
    public IQueryable<Vinyl> GetAllFullVinylsPaged(int currentPage, int pageSize)
    {
        return GetAllFullVinyls()
            .OrderBy(v => v.VinylId)
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize);
    }
    
    public IQueryable<Vinyl> FilterVinylsPaged(
            int currentPage,
            int pageSize,
            string? searchTerm,
            int? genreId,
            string? filterTitle,
            string? price)
    {
        var query = GetAllFullVinyls();
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(v => v.Title.Contains(searchTerm));
        }
        
        if (genreId.HasValue && genreId.Value > 0)
        {
            query = query.Where(v => v.VinylGenres != null && v.VinylGenres.Any(vg => vg.GenreId == genreId.Value));
        }
        
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
        return query
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize);
    }
    
    public enum SortDirection
    {
        Ascending,
        Descending
    }
    
    public IQueryable<Vinyl> FilterVinyls(
        string searchTerm,
        int? genreId,
        string titleSort,
        string priceSort)
    {
        SortDirection? titleSortDirection = ParseSortDirection(titleSort);
        SortDirection? priceSortDirection = ParseSortDirection(priceSort);
        
        IQueryable<Vinyl> query = _applicationDbContext.Vinyls
            .Include(v => v.VinylGenres)
            .ThenInclude(vg => vg.Genre);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(v => EF.Functions.Like(v.Title, $"%{searchTerm}%"));
        }

        if (genreId.HasValue && genreId.Value > 0)
        {
            query = query.Where(v => v.VinylGenres.Any(vg => vg.GenreId == genreId.Value));
        }

        if (titleSortDirection.HasValue)
        {
            query = titleSortDirection == SortDirection.Ascending
                ? query.OrderBy(v => v.Title)
                : query.OrderByDescending(v => v.Title);
        }

        if (priceSortDirection.HasValue)
        {
            query = priceSortDirection == SortDirection.Ascending
                ? query.OrderBy(v => v.Price)
                : query.OrderByDescending(v => v.Price);
        }

        return query;
    }
    
    private SortDirection? ParseSortDirection(string sortParameter)
    {
        return sortParameter switch
        {
            "Ascending" => SortDirection.Ascending,
            "Descending" => SortDirection.Descending,
            _ => null
        };
    }
    
    public PaginatedResult<Vinyl> GetPaginatedVinyls(int currentPage, int pageSize, string searchTerm, int? genreId, string titleSort, string priceSort)
    {
        var query = FilterVinyls(searchTerm, genreId, titleSort, priceSort);
        var totalRecords = query.Count();

        var items = query
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PaginatedResult<Vinyl>
        {
            Items = items,
            CurrentPage = currentPage,
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
        };
    }
}
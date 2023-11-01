using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Services;

public class VinylService : IVinylService
{
    private readonly ApplicationDbContext _ApplicationDbContext;

    public VinylService(ApplicationDbContext melodyMineService)
    {
        _ApplicationDbContext = melodyMineService;
    }
    
    public void CreateVinyl(Vinyl vinyl)
    {
        _ApplicationDbContext.Vinyls.Add(vinyl);

        _ApplicationDbContext.SaveChanges();
    }

    public void DeleteVinylById(int vinylId)
    {
        Vinyl tempVinyl = _ApplicationDbContext.Vinyls.FirstOrDefault(p => p.VinylId == vinylId);

        if (tempVinyl != null)
        {
            _ApplicationDbContext.Vinyls.Remove(tempVinyl);
            _ApplicationDbContext.SaveChanges();
        }
    }

    
    public Vinyl GetSingleVinylBy(int id)
    {
        Vinyl tempVinyl = _ApplicationDbContext.Vinyls
            .Where(p => p.VinylId == id)
            .FirstOrDefault();

        return tempVinyl;
    }
    
    public Vinyl GetSingleFullVinylBy(int id)
    {
        Vinyl tempVinyl = _ApplicationDbContext.Vinyls
            .Where(p => p.VinylId == id)
            .Include(p => p.Reviews)
            .Include(p => p.Covers)
            .Include(p => p.Genres)
            .Include(p => p.RecordLabel)
            .ThenInclude(m => m.Address)
            .FirstOrDefault();

        return tempVinyl;
    }
    
    public Vinyl GetSingleVinylBy(string title)
    {
        Vinyl tempVinyl = _ApplicationDbContext.Vinyls
            .Where(p => p.Title == title)
            .FirstOrDefault();

        return tempVinyl;
    }
    
    public Vinyl GetSingleFullVinylBy(string title)
    {
        Vinyl tempVinyl = _ApplicationDbContext.Vinyls
            .Where(p => p.Title == title)
            .Include(p => p.Reviews)
            .Include(p => p.Covers)
            .Include(p => p.Genres)
            .Include(p => p.RecordLabel)
            .ThenInclude(m => m.Address)
            .FirstOrDefault();

        return tempVinyl;
    }
    
    public void UpdateVinylBy(int vinylId, Vinyl newVinyl)
    {
        Vinyl tempVinyl = _ApplicationDbContext.Vinyls
            .Where(p => p.VinylId == vinylId)
            .FirstOrDefault();
        
        if (tempVinyl == null)
        {
            return;
        }

        tempVinyl.Title = newVinyl.Title;
        tempVinyl.Description = newVinyl.Description;
        tempVinyl.Price = newVinyl.Price;

        _ApplicationDbContext.SaveChanges();
    }
    
    public void UpdateVinylBy(string vinylTitle, Vinyl newVinyl)
    {
        Vinyl tempVinyl = _ApplicationDbContext.Vinyls
            .Where(p => p.Title == vinylTitle)
            .FirstOrDefault();
        
        if (tempVinyl == null)
        {
            return;
        }
        
        tempVinyl.Title = newVinyl.Title;
        tempVinyl.Price = newVinyl.Price;
        tempVinyl.Description = newVinyl.Description;
        _ApplicationDbContext.SaveChanges();
    }
    
    public IQueryable<Vinyl> GetAllVinyls()
    {
        IQueryable<Vinyl> tempVinyls = _ApplicationDbContext.Vinyls
            .Include(p => p.Covers)
            .Include(p => p.Reviews)
            .Include(c => c.Genres)
            .ThenInclude(cp => cp.Genre);

        return tempVinyls;
    }
    
    public IQueryable<Vinyl> GetAllVinylsPaged(int currentPage, int pageSize)
    {
        IQueryable<Vinyl> tempVinyls = _ApplicationDbContext.Vinyls
            .Include(p => p.Covers)
            .Include(p => p.Reviews)
            .Include(c => c.Genres)
            .ThenInclude(cp => cp.Genre);

        return tempVinyls.OrderBy(p => p.VinylId).Skip((currentPage - 1) * pageSize).Take(pageSize);
    }
    
    public IQueryable<Vinyl> GetAllFullVinyls()
    {
        IQueryable<Vinyl> tempVinyls = _ApplicationDbContext.Vinyls
            .Include(p => p.Reviews)
            .Include(p => p.Covers)
            .Include(p => p.Genres)
            .Include(p => p.RecordLabel)
            .ThenInclude(m => m.Address);

        return tempVinyls;
    }
    
    public IQueryable<Vinyl> GetAllFullVinylsPaged(int currentPage, int pageSize)
    {
        IQueryable<Vinyl> tempVinyls = _ApplicationDbContext.Vinyls
            .Include(p => p.Reviews)
            .Include(p => p.Covers)
            .Include(p => p.Genres)
            .Include(p => p.RecordLabel)
            .ThenInclude(m => m.Address);

        return tempVinyls.OrderBy(p => p.VinylId).Skip((currentPage - 1) * pageSize).Take(pageSize);
    }
    
    public IQueryable<Vinyl> FilterVinylsPaged(int currentPage, int pageSize, string? SearchTerm, int? GenreId, string? FilterTitle, string? Price)
    {
        IQueryable<Vinyl> tempVinyls = _ApplicationDbContext.Vinyls
                .Include(p => p.Covers)
                .Include(p => p.Reviews)
                .Include(c => c.Genres)
                .ThenInclude(cp => cp.Genre);

        if (!string.IsNullOrWhiteSpace(SearchTerm))
        {
            tempVinyls = tempVinyls.Where(p => p.Title.Contains(SearchTerm));
        }

        if (GenreId != null && GenreId != 0)
        {
            IQueryable<VinylGenre> tempVinylsSecond = _ApplicationDbContext.VinylGenres.Where(p => p.GenreId == GenreId);
            List<Vinyl> tempVinylsThird = new List<Vinyl>();

            foreach (VinylGenre vinylGenre in tempVinylsSecond.ToList())
            {
                tempVinylsThird.Add(tempVinyls.Where(p => p.VinylId == vinylGenre.VinylId).FirstOrDefault());
            }
            tempVinyls = tempVinylsThird.AsQueryable();
        }

        if (!string.IsNullOrWhiteSpace(FilterTitle))
        {
            if (FilterTitle == "+")
            {
                tempVinyls = tempVinyls.OrderBy(p => p.Title);
            }
            else
            {
                tempVinyls = tempVinyls.OrderByDescending(p => p.Title);
            }
        }

        if (!string.IsNullOrWhiteSpace(Price))
        {
            if (FilterTitle == "+")
            {
                tempVinyls = tempVinyls.OrderBy(p => p.Price);
            }
            else
            {
                tempVinyls = tempVinyls.OrderBy(p => p.Price).Reverse();
            }
        }

        List<Vinyl> vinyls = tempVinyls.ToList();
        vinyls.RemoveAll(item => item == null);
        tempVinyls = vinyls.AsQueryable();

        return tempVinyls.Skip((currentPage - 1) * pageSize).Take(pageSize);
    }
    
    public IQueryable<Vinyl> FilterVinyls(string? SearchTerm, int? GenreId, string? FilterTitle, string? Price)
    {
        IQueryable<Vinyl> tempVinyls = _ApplicationDbContext.Vinyls
            .Include(p => p.Covers)
            .Include(p => p.Reviews)
            .Include(c => c.Genres)
            .ThenInclude(cp => cp.Genre);

        if (!string.IsNullOrWhiteSpace(SearchTerm))
        {
            tempVinyls = tempVinyls.Where(p => p.Title.Contains(SearchTerm));
        }
        if (GenreId != null && GenreId != 0)
        {
            IQueryable<VinylGenre> tempVinylsSecond = _ApplicationDbContext.VinylGenres.Where(p => p.GenreId == GenreId);
            List<Vinyl> tempVinylsThird = new List<Vinyl>();

            foreach (VinylGenre vinylGenre in tempVinylsSecond.ToList())
            {
                tempVinylsThird.Add(tempVinyls.Where(p => p.VinylId == vinylGenre.VinylId).FirstOrDefault());
            }
            tempVinyls = tempVinylsThird.AsQueryable();
        }
        if (!string.IsNullOrWhiteSpace(FilterTitle))
        {
            if (FilterTitle == "+")
            {
                tempVinyls = tempVinyls.OrderBy(p => p.Title);
            }
            else
            {
                tempVinyls = tempVinyls.OrderByDescending(p => p.Title);
            }
        }
        if (!string.IsNullOrWhiteSpace(Price))
        {
            if (FilterTitle == "+")
            {
                tempVinyls = tempVinyls.OrderBy(p => p.Price);
            }
            else
            {
                tempVinyls = tempVinyls.OrderByDescending(p => p.Price);
            }
        }

        return tempVinyls;
    }
}
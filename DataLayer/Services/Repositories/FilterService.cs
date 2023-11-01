using System.Collections.Generic;
using System.Linq;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Services;

public class FilterService : IFilterService
{
    private readonly ApplicationDbContext _ApplicationDbContext;

    public FilterService(ApplicationDbContext melodyMineService)
    {
        _ApplicationDbContext = melodyMineService;
    }
    

    #region Filters
    /*
    public IQueryable<VinylDTO> FilterVinylsSimpel(string? SearchTerm, string? FilterTitle, string? Price)
    {
        List<VinylDTO> tempVinyls = new List<VinylDTO>();
        foreach (Vinyl vinyl in _ApplicationDbContext.Vinyls)
        {
            tempVinyls.Add(
                new VinylDTO 
                {
                    VinylId = vinyl.VinylId,
                    Title = vinyl.Title,
                    Description = vinyl.Description,
                    Price = vinyl.Price,
                    RecordLabelId = vinyl.RecordLabelId
                });
        }
        IQueryable<VinylDTO> query = tempVinyls.AsQueryable();

        if (!string.IsNullOrWhiteSpace(SearchTerm))
        {
            query = query.Where(p => p.Title.Contains(SearchTerm));
        }
        if (!string.IsNullOrWhiteSpace(FilterTitle))
        {
            if (FilterTitle == "+")
            {
                query = query.OrderBy(p => p.ProductName);
            }
            else
            {
                query = query.OrderByDescending(p => p.ProductName);
            }
        }
        if (!string.IsNullOrWhiteSpace(Price))
        {
            if (FilterTitle == "+")
            {
                query = query.OrderBy(p => p.Price);
            }
            else
            {
                query = query.OrderByDescending(p => p.Price);
            }
        }

        return query;
    }
    */

    #endregion
}
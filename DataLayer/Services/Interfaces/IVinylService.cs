using DataLayer.Models;

namespace DataLayer.Services;

public interface IVinylService
{
    public void CreateVinyl(Vinyl vinyl);
    public void DeleteVinylById(int VinylId);
    public Vinyl GetVinylById(int id);
    public Vinyl GetSingleFullVinylBy(int id);
    public Vinyl GetSingleVinylBy(string title);
    public Vinyl GetSingleFullVinylBy(string title);
    public void UpdateVinylBy(int vinylId, Vinyl newVinyl);
    public void UpdateVinylBy(string vinylTitle, Vinyl newVinyl);
    public IQueryable<Vinyl> GetAllVinyls();
    public IQueryable<Vinyl> GetAllVinylsPaged(int currentPage, int pageSize);
    public IQueryable<Vinyl> GetAllFullVinyls();
    public IQueryable<Vinyl> GetAllFullVinylsPaged(int currentPage, int pageSize);
    public IQueryable<Vinyl> FilterVinylsPaged(int currentPage, int pageSize, string? SearchTerm, int? GenreId, string? FilterTitle, string? Price);
    public IQueryable<Vinyl> FilterVinyls(string? SearchTerm, int? GenreId, string? FilterTitle, string? Price);
}
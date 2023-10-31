using System.Collections.Generic;
using System.Linq;
using DataLayer.Models;

namespace DataLayer.Services;

public interface IMelodyMineService
{
    #region Genre
    public IQueryable<Genre> GetAllGenres();
    public IQueryable<Genre> GetGenresById(int genreId);

    public IQueryable<VinylGenre> GetAllVinylGenres();
    public void CreateVinylGenre(int VinylId, int GenreId);
    #endregion

    #region Vinyls
    public void CreateVinyl(Vinyl vinyl);
    public void DeleteVinylById(int VinylId);
    public Vinyl GetSingleVinylBy(int id);
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
    // public IQueryable<Vinyl> FilterVinyls(string? SearchTerm, int? GenreId, string? FilterTitle, string? Price);
    // public IQueryable<VinylDTO> FilterVinylsSimpel(string? SearchTerm, string? FilterTitle, string? Price);
    public void CreateOrder(Order order);
    public void CreateOrderProductDetails(List<OrderProductDetails> orderProductDetails);
    public void DeleteOrder(Order order);
    public Order GetSingleOrderBy(int id);
    public Order GetSingleFullOrderBy(int id);
    public Order GetSingleOrderBy(string email);
    public Order GetSingleFullOrderBy(string email);
    public void UpdateVinylBy(int orderId, Order newOrder);
    public void UpdateOrderBy(string orderEmail, Order newOrder);
    public IQueryable<Order> GetAllOrders();
    public IQueryable<Order> GetAllFullOrders();
    public IQueryable<RecordLabel> GetRecordLabelById(int recordLabelId);
    public RecordLabel GetRecordLabelByIdSimple(int recordLabelId);
    public IQueryable<RecordLabel> GetAllRecordLabels();
    public void CreateRecordLabel(RecordLabel recordLabel);
    public void DeleteRecordLabel(RecordLabel recordLabel);
    public bool LogIn(Admin admin);
    public bool LogOut(Admin admin);
    public bool SignedIn(string username);
    public bool AnySignedIn();
    public IQueryable<Review> getReviewsByVinylID(int vinylId);
    public void CreateReview(Review review);
    #endregion
}
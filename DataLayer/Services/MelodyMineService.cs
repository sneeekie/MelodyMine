using System.Collections.Generic;
using System.Linq;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Services;

public class MelodyMineService : IMelodyMineService
{
    private readonly ApplicationDbContext _ApplicationDbContext;

    public MelodyMineService(ApplicationDbContext melodyMineService)
    {
        _ApplicationDbContext = melodyMineService;
    }

    #region Genre
    public IQueryable<Genre> GetAllGenres()
    {
        IQueryable<Genre> tempGenres = _ApplicationDbContext.Genres;

        return tempGenres;

    }
    
    public IQueryable<Genre> GetGenresById(int genreId)
    {
        IQueryable<Genre> tempGenres = _ApplicationDbContext.Genres.Where(c => c.GenreId == genreId);

        return tempGenres;
    }
    
    public IQueryable<VinylGenre> GetAllVinylGenres()
    {
        IQueryable<VinylGenre> tempGenres = _ApplicationDbContext.vinylGenres;
            
        return tempGenres;
    }
    
    public void CreateVinylGenre(int VinylId, int GenreId)
    {
        _ApplicationDbContext.vinylGenres.Add(new VinylGenre { VinylId = VinylId, GenreId = GenreId });
        _ApplicationDbContext.SaveChanges();
    }
    #endregion

    #region Vinyls
    
    public void CreateVinyl(Vinyl vinyl)
    {
        _ApplicationDbContext.Vinyls.Add(vinyl);

        _ApplicationDbContext.SaveChanges();
    }

    public void DeleteVinylById(int vinylId)
    {
        Vinyl tempVinyl = _ApplicationDbContext.Vinyls.Where(p => p.VinylId == vinylId).FirstOrDefault();
        _ApplicationDbContext.Vinyls.Remove(tempVinyl);
        _ApplicationDbContext.SaveChanges();
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
        tempVinyl = newVinyl;
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
            IQueryable<VinylGenre> tempVinylsSecond = _ApplicationDbContext.vinylGenres.Where(p => p.GenreId == GenreId);
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
            IQueryable<VinylGenre> tempVinylsSecond = _ApplicationDbContext.vinylGenres.Where(p => p.GenreId == GenreId);
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

    #region Orders
    
    public void CreateOrder(Order order)
    {
        _ApplicationDbContext.Orders.Add(order);

        _ApplicationDbContext.SaveChanges();
    }

    public void CreateOrderProductDetails(List<OrderProductDetails> orderProductsDetails)
    {
        foreach (OrderProductDetails opd in orderProductsDetails)
        {
            _ApplicationDbContext.OrderProductDetails.Add(opd);
        }
        _ApplicationDbContext.SaveChanges();
    }
    
    public void DeleteOrder(Order order)
    {
        _ApplicationDbContext.Orders.Remove(order);
        _ApplicationDbContext.SaveChanges();
    }
    
    public Order GetSingleOrderBy(int id)
    {
        Order tempOrder = _ApplicationDbContext.Orders
            .Where(o => o.OrderId == id)
            .FirstOrDefault();

        return tempOrder;
    }
    
    public Order GetSingleFullOrderBy(int id)
    {
        Order tempOrder = _ApplicationDbContext.Orders
            .Where(o => o.OrderId == id)
            .Include(o => o.OrderProductDetails)
            .FirstOrDefault();

        return tempOrder;
    }
    
    public Order GetSingleOrderBy(string email)
    {
        Order tempOrder = _ApplicationDbContext.Orders
            .Where(o => o.Email == email)
            .FirstOrDefault();

        return tempOrder;
    }
    public Order GetSingleFullOrderBy(string email)
    {
        Order tempOrder = _ApplicationDbContext.Orders
            .Where(o => o.Email == email)
            .Include(o => o.OrderProductDetails)
            .FirstOrDefault();

        return tempOrder;
    }
    
    public void UpdateVinylBy(int orderId, Order newOrder)
    {
        Order tempOrder = _ApplicationDbContext.Orders
            .Where(o => o.OrderId == orderId)
            .FirstOrDefault();
        tempOrder = newOrder;
        _ApplicationDbContext.SaveChanges();
    }
    
    public void UpdateOrderBy(string orderEmail, Order newOrder)
    {
        Order tempOrder = _ApplicationDbContext.Orders
            .Where(o => o.Email == orderEmail)
            .FirstOrDefault();
        tempOrder = newOrder;
        _ApplicationDbContext.SaveChanges();
    }
    
    public IQueryable<Order> GetAllOrders()
    {
        IQueryable<Order> tempOrders = _ApplicationDbContext.Orders;

        return tempOrders;
    }
    public IQueryable<Order> GetAllFullOrders()
    {
        IQueryable<Order> tempOrders = _ApplicationDbContext.Orders
            .Include(o => o.OrderProductDetails);

        return tempOrders;
    }
    
    #endregion

    #region RecordLabel

    public IQueryable<RecordLabel> GetRecordLabelById(int recordLabelId)
    {
        IQueryable<RecordLabel> tempRecordLabel = _ApplicationDbContext.RecordLabels
            .Include(i => i.Address)
            .Where(mId => mId.RecordLabelId == recordLabelId);


        return tempRecordLabel;
    }
    
    public RecordLabel GetRecordLabelByIdSimple(int recordLabelId)
    {
        RecordLabel tempRecordLabel = _ApplicationDbContext.RecordLabels
            .Where(mId => mId.RecordLabelId == recordLabelId)
            .FirstOrDefault();


        return tempRecordLabel;
    }
    
    public IQueryable<RecordLabel> GetAllRecordLabels()
    {
        return _ApplicationDbContext.RecordLabels;
    }
    
    public void CreateRecordLabel(RecordLabel recordLabel)
    {
        _ApplicationDbContext.RecordLabels.Add(recordLabel);
        _ApplicationDbContext.SaveChanges();
    }
    
    public void DeleteRecordLabel(RecordLabel recordLabel)
    {
        _ApplicationDbContext.RecordLabels.Remove(recordLabel);
        _ApplicationDbContext.SaveChanges();
    }
    #endregion

    #region Admin

    public bool LogIn(Admin admin)
    {
        Admin TempAadmin = _ApplicationDbContext.Admins
            .Where(a => a.Username == admin.Username)
            .Where(a => a.Password == admin.Password)
            .FirstOrDefault();
        if (TempAadmin != null)
        {
            TempAadmin.SignedIn = true;
            _ApplicationDbContext.SaveChanges();
            return true;
        }
        return false;
    }
    
    public bool LogOut(Admin admin)
    {
        Admin tempAadmin = _ApplicationDbContext.Admins
            .Where(a => a.Username == admin.Username)
            .FirstOrDefault();

        if (tempAadmin != null)
        {
            tempAadmin.SignedIn = false;
            _ApplicationDbContext.SaveChanges();
            return true;
        }
        return false;
    }
    
    public bool SignedIn(string username)
    {
        if (_ApplicationDbContext.Admins.Where(a => a.Username == username).Where(a => a.SignedIn == true).FirstOrDefault() != null)
        {
            return true;
        }
        return false;
    }
    
    public bool AnySignedIn()
    {
        Admin admin = _ApplicationDbContext.Admins.Where(a => a.SignedIn == true).FirstOrDefault();
        if (admin != null)
        {
            return true;
        }
        return false;
    }
    #endregion

    #region Review

    public IQueryable<Review> getReviewsByVinylID(int vinylId)
    {
        IQueryable<Review> tempReviews = _ApplicationDbContext.Reviews
            .Where(p => p.VinylId == vinylId);
        return tempReviews;
    }
    public void CreateReview(Review review)
    {
        _ApplicationDbContext.Reviews.Add(review);

        _ApplicationDbContext.SaveChanges();
    }
    #endregion
}
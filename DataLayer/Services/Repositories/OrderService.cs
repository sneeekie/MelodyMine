using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public OrderService(ApplicationDbContext melodyMineService)
    {
        _applicationDbContext = melodyMineService;
    }
    
    public void CreateOrder(Order? order)
    {
        _applicationDbContext.Orders.Add(order);

        _applicationDbContext.SaveChanges();
    }

    public void CreateOrderProductDetails(List<OrderProductDetails> orderProductsDetails)
    {
        foreach (OrderProductDetails opd in orderProductsDetails)
        {
            _applicationDbContext.OrderProductDetails.Add(opd);
        }
        _applicationDbContext.SaveChanges();
    }
    
    public int CreateAddress(Address address)
    {
        _applicationDbContext.Addresses.Add(address);
        _applicationDbContext.SaveChanges();
        
        return address.AddressId;
    }
    
    public void DeleteOrder(Order? order)
    {
        var existingOrder = _applicationDbContext.Orders.Find(order.OrderId);
        if (existingOrder != null)
        {
            _applicationDbContext.Orders.Remove(existingOrder);
            _applicationDbContext.SaveChanges();
        }
    }
    
    public Order? GetSingleOrderBy(int id)
    {
        Order? tempOrder = _applicationDbContext.Orders
            .FirstOrDefault(o => o.OrderId == id);

        return tempOrder;
    }
    
    public Order GetSingleFullOrderBy(int id)
    {
        return _applicationDbContext.Orders
            .Include(o => o.OrderProductDetails)
            .Include(o => o.Address)
            .FirstOrDefault(o => o.OrderId == id);
    }
    
    public Order GetSingleOrderBy(string email)
    {
        Order tempOrder = _applicationDbContext.Orders
            .FirstOrDefault(o => o.Email == email);

        return tempOrder;
    }
    
    public Order GetSingleFullOrderBy(string email)
    {
        return _applicationDbContext.Orders
            .Include(o => o.OrderProductDetails)
            .Include(o => o.Address)
            .FirstOrDefault(o => o.Email == email);
    }

    public void UpdateOrderById(int orderId, Order newOrder)
    {
        Order tempOrder = _applicationDbContext.Orders
            .FirstOrDefault(o => o.OrderId == orderId);
    
        if (tempOrder == null)
        {
            return;
        }

        tempOrder.Email = newOrder.Email;
        tempOrder.BuyDate = newOrder.BuyDate;
        _applicationDbContext.SaveChanges();
    }
    
    public void UpdateOrderByEmail(string orderEmail, Order newOrder)
    {
        Order tempOrder = _applicationDbContext.Orders
            .FirstOrDefault(o => o.Email == orderEmail);

        if (tempOrder == null)
        {
            return;
        }

        tempOrder.Email = newOrder.Email;
        tempOrder.BuyDate = newOrder.BuyDate;
        _applicationDbContext.SaveChanges();
    }

    
    public IQueryable<Order?> GetAllOrders()
    {
        IQueryable<Order?> tempOrders = _applicationDbContext.Orders;

        return tempOrders;
    }
    
    public IQueryable<Order> GetAllFullOrders()
    {
        IQueryable<Order> tempOrders = _applicationDbContext.Orders
            .Include(o => o.OrderProductDetails);

        return tempOrders;
    }
    
    public void UpdateAddress(int addressId, Address newAddress)
    {
        var address = _applicationDbContext.Addresses.Find(addressId);
        if (address != null)
        {
            _applicationDbContext.Entry(address).CurrentValues.SetValues(newAddress);
            _applicationDbContext.SaveChanges();
        }
    }
    
    public void UpdateOrderProductDetails(List<OrderProductDetails> newOrderProductDetails)
    {
        foreach (var opd in newOrderProductDetails)
        {
            var existingOpd = _applicationDbContext.OrderProductDetails.Find(opd.OrderProductDetailsId);
            if (existingOpd != null)
            {
                _applicationDbContext.Entry(existingOpd).CurrentValues.SetValues(opd);
            }
        }
        _applicationDbContext.SaveChanges();
    }
    
    public bool OrderExists(int id)
    {
        return _applicationDbContext.Orders.Any(e => e.OrderId == id);
    }
    
}
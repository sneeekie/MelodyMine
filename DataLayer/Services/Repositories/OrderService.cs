using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _ApplicationDbContext;

    public OrderService(ApplicationDbContext melodyMineService)
    {
        _ApplicationDbContext = melodyMineService;
    }
    
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
        var existingOrder = _ApplicationDbContext.Orders.Find(order.OrderId);
        if (existingOrder != null)
        {
            _ApplicationDbContext.Orders.Remove(existingOrder);
            _ApplicationDbContext.SaveChanges();
        }
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
    
        if (tempOrder == null)
        {
            return;
        }

        tempOrder.Email = newOrder.Email;
        tempOrder.BuyDate = newOrder.BuyDate;
        _ApplicationDbContext.SaveChanges();
    }
    
    public void UpdateOrderBy(string orderEmail, Order newOrder)
    {
        Order tempOrder = _ApplicationDbContext.Orders
            .Where(o => o.Email == orderEmail)
            .FirstOrDefault();

        if (tempOrder == null)
        {
            return;
        }

        tempOrder.Email = newOrder.Email;
        tempOrder.BuyDate = newOrder.BuyDate;
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
    
}
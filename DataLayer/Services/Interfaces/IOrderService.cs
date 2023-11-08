using DataLayer.Models;

namespace DataLayer.Services;

public interface IOrderService
{
    public void CreateOrder(Order order);
    public void CreateOrderProductDetails(List<OrderProductDetails> orderProductDetails);
    public int CreateAddress(Address address);
    public void DeleteOrder(Order order);
    public Order GetSingleOrderBy(int id);
    public Order GetSingleFullOrderBy(int id);
    public Order GetSingleOrderBy(string email);
    public Order GetSingleFullOrderBy(string email);
    public void UpdateVinylBy(int orderId, Order newOrder);
    public void UpdateOrderBy(string orderEmail, Order newOrder);
    public IQueryable<Order> GetAllOrders();
    public IQueryable<Order> GetAllFullOrders();
}
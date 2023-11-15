using DataLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MelodyMine.Pages;

public class OrdersModel : PageModel
{
    private readonly IOrderService _orderService;

    public IList<Order?> Orders { get; private set; }

    public OrdersModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public void OnGet()
    {
        Orders = _orderService.GetAllOrders().ToList();
    }

    public IActionResult OnPostDelete(int id)  
    {  
        var orderToDelete = _orderService.GetSingleOrderBy(id);  
        _orderService.DeleteOrder(orderToDelete);  
  
        return RedirectToPage();  
    }
}
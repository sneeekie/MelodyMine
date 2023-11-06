using DataLayer.Models;
using DataLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class OrdersModel : PageModel
{
    private readonly IOrderService _orderService;

    public IList<Order> Orders { get; private set; }

    [BindProperty]
    public Order NewOrder { get; set; }

    [BindProperty]
    public Address NewAddress { get; set; }

    public OrdersModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public void OnGet()
    {
        Orders = _orderService.GetAllOrders().ToList();
    }

    public IActionResult OnPostCreate()
    {
        if (!ModelState.IsValid)
        {
            Orders = _orderService.GetAllOrders().ToList();
            return Page();
        }
        
        NewOrder.AddressId = _orderService.CreateAddress(NewAddress);
        
        _orderService.CreateOrder(NewOrder);

        return RedirectToPage();
    }

    public IActionResult OnPostDelete(int id)  
    {  
        var orderToDelete = _orderService.GetSingleOrderBy(id);  
        if (orderToDelete == null)  
        {        return NotFound();  
        }  
        _orderService.DeleteOrder(orderToDelete);  
  
        return RedirectToPage();  
    }
}
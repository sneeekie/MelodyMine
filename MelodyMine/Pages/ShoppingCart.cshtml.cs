using DataLayer.Models;
using DataLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MelodyMine.Pages;

public class ShoppingCartModel : PageModel
{
    private readonly IOrderService _orderService;

    public ShoppingCartModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [BindProperty]
    public Address NewAddress { get; set; } = new Address();

    [BindProperty]
    public Order NewOrder { get; set; } = new Order();

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        int addressId = _orderService.CreateAddress(NewAddress);
        
        NewOrder.AddressId = addressId;
        NewOrder.BuyDate = DateTime.UtcNow;
        
        _orderService.CreateOrder(NewOrder);
        int orderId = NewOrder.OrderId;

        
        return RedirectToPage("/Complete", new { orderId = orderId });

    }
}
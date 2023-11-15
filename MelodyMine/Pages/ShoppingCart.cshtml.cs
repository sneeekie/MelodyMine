using DataLayer.Models;
using DataLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MelodyMine.Pages;

public class ShoppingCartModel : PageModel
{
    private readonly IOrderService _orderService;
    private readonly IVinylService _vinylService;

    public ShoppingCartModel(IOrderService orderService, VinylService vinylService)
    {
        _orderService = orderService;
        _vinylService = vinylService;
    }

    [BindProperty]
    public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

    [BindProperty]
    public Order NewOrder { get; set; } = new Order();

    [BindProperty]
    public Address NewAddress { get; set; } = new Address();

    public void OnGet()
    {
        ShoppingCartItems = GetShoppingCartItems();
    }

    public IActionResult OnPostAddToCart(int vinylId, int quantity)
    {
        ShoppingCartItems = GetShoppingCartItems();
        var existingItem = ShoppingCartItems.Find(item => item.VinylId == vinylId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            var vinyl = _vinylService.GetVinylById(vinylId);
            if (vinyl != null)
            {
                ShoppingCartItems.Add(new ShoppingCartItem 
                { 
                    VinylId = vinyl.VinylId, 
                    Title = vinyl.Title, 
                    Price = vinyl.Price, 
                    Quantity = quantity 
                });
            }
            else
            {
                TempData["Error"] = "The selected vinyl does not exist.";
                return RedirectToPage();
            }
        }

        SaveCartSession();
        return RedirectToPage();
    }

    public IActionResult OnPostRemoveFromCart(int vinylId)
    {
        ShoppingCartItems = GetShoppingCartItems();
        var itemToRemove = ShoppingCartItems.SingleOrDefault(r => r.VinylId == vinylId);
        if (itemToRemove != null)
        {
            ShoppingCartItems.Remove(itemToRemove);
        }

        SaveCartSession();
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostCheckoutAsync()
    {
        ShoppingCartItems = GetShoppingCartItems();
        
        var addressId = _orderService.CreateAddress(NewAddress);
        NewOrder.AddressId = addressId;
        
        
        var orderDetails = ShoppingCartItems.Select(item => new OrderProductDetails
        {
            VinylId = item.VinylId,
            Quantity = item.Quantity,
            Price = item.Price
        }).ToList();
        
        _orderService.CreateOrder(NewOrder);

        orderDetails.ForEach(e =>
        {
            e.OrderId = NewOrder.OrderId;
        });
        
        _orderService.AddProductDetails(orderDetails);
        
        ClearCartSession();
        
        return RedirectToPage("Complete", new{ orderId = NewOrder.OrderId } );
    }

    private List<ShoppingCartItem> GetShoppingCartItems()
    {
        var sessionCart = HttpContext.Session.GetString("ShoppingCart");
        return sessionCart != null
            ? JsonConvert.DeserializeObject<List<ShoppingCartItem>>(sessionCart)
            : new List<ShoppingCartItem>();
    }

    private void SaveCartSession()
    {
        var json = JsonConvert.SerializeObject(ShoppingCartItems);
        HttpContext.Session.SetString("ShoppingCart", json);
    }

    private void ClearCartSession()
    {
        HttpContext.Session.Remove("ShoppingCart");
    }
}

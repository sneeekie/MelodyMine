using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataLayer.Models;
using DataLayer.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MelodyMine.Pages;

public class EditOrderModel : PageModel
{
    private readonly IOrderService _orderService;

    [BindProperty]
    public Order Order { get; set; }

    [BindProperty]
    public List<OrderProductDetails> OrderProductDetails { get; set; }

    public EditOrderModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Order = _orderService.GetSingleFullOrderBy(id);

        OrderProductDetails = new List<OrderProductDetails>(Order.OrderProductDetails);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            _orderService.UpdateAddress(Order.AddressId, Order.Address);
            _orderService.UpdateOrderById(Order.OrderId, Order);
            _orderService.UpdateOrderProductDetails(OrderProductDetails);

            return RedirectToPage("./Orders");
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_orderService.OrderExists(Order.OrderId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
    }
}
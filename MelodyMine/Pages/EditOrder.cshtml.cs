using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataLayer.DTOs;
using DataLayer.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MelodyMine.Pages;

public class EditOrderModel : PageModel
{
    private readonly IOrderService _orderService;

    [BindProperty]
    public OrderDto Order { get; set; }
    
    public EditOrderModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Order = _orderService.GetOrderDtoById(id);
        if (Order == null)
        {
            return NotFound();
        }

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
            _orderService.UpdateOrderDto(Order);

            return RedirectToPage("./Orders");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while updating the order: " + ex.Message);
            return Page();
        }
    }
}
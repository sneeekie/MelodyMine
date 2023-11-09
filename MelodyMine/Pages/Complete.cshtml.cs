using DataLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MelodyMine.Pages;

public class CompleteModel : PageModel
{
    private readonly IOrderService _orderService;

    public CompleteModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public Order Order { get; private set; }

    public void OnGet(int orderId)
    {
        Order = _orderService.GetSingleFullOrderBy(orderId);

        if (Order == null)
        {
        }
        
    }
}
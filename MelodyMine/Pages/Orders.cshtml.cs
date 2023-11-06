using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataLayer.Models;
using DataLayer.Services;
using System.Collections.Generic;
using System.Linq;

namespace MelodyMine.Pages
{
    public class OrdersModel : PageModel
    {
        private readonly IOrderService _orderService;

        public IList<Order> Orders { get; private set; }

        [BindProperty]
        public Order NewOrder { get; set; }

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
                return Page();
            }

            _orderService.CreateOrder(NewOrder);

            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            var orderToDelete = _orderService.GetSingleOrderBy(id);
            if (orderToDelete == null)
            {
                return NotFound();
            }

            _orderService.DeleteOrder(orderToDelete);

            return RedirectToPage();
        }
    }
}
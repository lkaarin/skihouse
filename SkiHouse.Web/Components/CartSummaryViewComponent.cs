using Microsoft.AspNetCore.Mvc;
using SkiHouse.Web.Data.Models;

namespace SkiHouse.Web.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly Cart _cartService;

        public CartSummaryViewComponent(Cart cartService)
        {
            _cartService = cartService;
        }

        public IViewComponentResult Invoke() => View(_cartService);
    }
}

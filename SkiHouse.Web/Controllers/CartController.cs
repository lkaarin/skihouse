using System.Linq;

using SkiHouse.Web.Data.Models;
using SkiHouse.Web.Data.Repositories.Interfaces;
using SkiHouse.Web.Infrastructure.Extensions;

using Microsoft.AspNetCore.Mvc;
using SkiHouse.Web.ViewModels;

namespace SkiHouse.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private Cart _cartService;
        public CartController(IProductRepository productRepository, Cart cartService)
        {
            _productRepository = productRepository;
            _cartService = cartService;
        }

        public ViewResult Index(string returnUrl)=> View(new CartIndexViewModel
        {
            Cart = _cartService,
            ReturnUrl = returnUrl
        });

        public IActionResult AddToCart(int productId, string returnUrl)
        {
            var product = _productRepository.Products.FirstOrDefault(x => x.ProductId == productId);

            if (product != null)
            {
                _cartService.AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl});
        }

        public IActionResult RemoveFromCart(int productId, string returnUrl)
        {
            var product = _productRepository.Products.FirstOrDefault(x => x.ProductId == productId);

            if (product != null)
            {
                _cartService.RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }
    }
}
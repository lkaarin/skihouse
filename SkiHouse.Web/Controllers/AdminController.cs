using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkiHouse.Web.Data.Models;
using SkiHouse.Web.Data.Repositories.Interfaces;
using System.Linq;

namespace SkiHouse.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository _productRepository;

        public AdminController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ViewResult Index() => View(_productRepository.Products);

        public ViewResult Edit(int productId) => View(
            _productRepository.Products
                .FirstOrDefault(p => p.ProductId == productId));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public IActionResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            var deletedProduct = _productRepository.DeleteProduct(productId);
            if(deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} has been deleted";
            }

            return RedirectToAction("Index");
        }
    }
}

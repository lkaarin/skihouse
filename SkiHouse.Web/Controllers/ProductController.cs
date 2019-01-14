using Microsoft.AspNetCore.Mvc;
using SkiHouse.Web.Data.Models;
using SkiHouse.Web.Data.Repositories.Interfaces;
using SkiHouse.Web.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace SkiHouse.Web.Controllers
{
    public class ProductController : Controller
    {
        private const int PageSize = 4;
        private readonly IProductRepository _productRepository;
        
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult List(string category = null, int page = 1) => View(
            new ProductListViewModel
            {
                Items = _productRepository.Products.
                    Where(x=>string.IsNullOrEmpty(category) || x.Category == category).
                    Skip(PageSize * (page - 1)).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _productRepository.Products.Count(x => string.IsNullOrEmpty(category) || x.Category == category)
                },
                CurrentCategory = category
            });
    }
}
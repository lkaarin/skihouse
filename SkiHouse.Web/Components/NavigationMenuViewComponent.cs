using System.Collections.Generic;
using System.Linq;

using SkiHouse.Web.Data.Repositories.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace SkiHouse.Web.Components
{
    public class NavigationMenuViewComponent: ViewComponent
    {
        private readonly IProductRepository _productRepository;

        public NavigationMenuViewComponent(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            return View(_productRepository.Products.
                Select(x => x.Category).
                Distinct().
                Where(x => !string.IsNullOrEmpty(x)).
                OrderBy(x => x));
        }
    }
}

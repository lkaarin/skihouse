using SkiHouse.Web.Data.Models;
using SkiHouse.Web.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkiHouse.Web.Data.Repositories
{
    public class MemoryProductRepository /*: IProductRepository*/
    {
        private static IEnumerable<Product> _products = new List<Product>
        {
            new Product{Name = "iPhone XS", Price = 2000},
            new Product{Name="Mate 10 Pro", Price = 800}
        };

        public IEnumerable<Product> Products { get; } = _products;
    }
}

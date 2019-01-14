using SkiHouse.Web.Data.Models;
using SkiHouse.Web.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkiHouse.Web.Data.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private SkiHouseDBContext _skiHouseDBContext;

        public IEnumerable<Product> Products => _skiHouseDBContext.Products;

        public EFProductRepository(SkiHouseDBContext skiHouseDBContext)
        {
            _skiHouseDBContext = skiHouseDBContext;
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                _skiHouseDBContext.Products.Add(product);
            }
            else
            {
                Product dbEntry = _skiHouseDBContext.Products
                    .FirstOrDefault(p => p.ProductId == product.ProductId);

                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }

            _skiHouseDBContext.SaveChanges();
        }

        public Product DeleteProduct(int productId)
        {
            var dbEntry = _skiHouseDBContext.Products.FirstOrDefault(x => x.ProductId == productId);

            if (dbEntry != null)
            {
                _skiHouseDBContext.Products.Remove(dbEntry);
                _skiHouseDBContext.SaveChanges();
            }

            return dbEntry;
        }
    }
}

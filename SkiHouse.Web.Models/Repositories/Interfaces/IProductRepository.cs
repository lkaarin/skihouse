using SkiHouse.Web.Data.Models;
using System.Collections.Generic;

namespace SkiHouse.Web.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        void SaveProduct(Product product);
        Product DeleteProduct(int productId);
    }
}

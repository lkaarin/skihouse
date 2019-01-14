using System.Linq;

using SkiHouse.Web.Data.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SkiHouse.Web.Data
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            var dBContext = app.ApplicationServices.GetRequiredService<SkiHouseDBContext>();

            if (!dBContext.Products.Any())
            {
                dBContext.Products.AddRange(
                    new Product { Name = "iPhone XS", Price = 2000, Category="iPhone" },
                    new Product { Name = "Mate 10 Pro", Price = 800, Category = "Huawei" },
                    new Product { Name = "P20", Price = 700, Category = "Huawei" },
                    new Product { Name = "P20 Lite", Price = 250, Category = "Huawei" },
                    new Product { Name = "Asus Zenfone3", Price = 300, Category = "Asus" }
                );

                dBContext.SaveChanges();
            }
        }
    }
}

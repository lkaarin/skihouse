using Microsoft.EntityFrameworkCore;
using SkiHouse.Web.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkiHouse.Web.Data
{
    public class SkiHouseDBContext: DbContext
    {
        public SkiHouseDBContext(DbContextOptions<SkiHouseDBContext> options) : base(options) { }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}

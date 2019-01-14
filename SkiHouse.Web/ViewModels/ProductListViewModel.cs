using SkiHouse.Web.Data.Models;
using System.Collections.Generic;

namespace SkiHouse.Web.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Items { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}

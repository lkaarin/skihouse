using Microsoft.EntityFrameworkCore;
using SkiHouse.Web.Data.Models;
using SkiHouse.Web.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkiHouse.Web.Data.Repositories
{
    public class EFOrderRepository: IOrderRepository
    {
        private readonly SkiHouseDBContext _skiHouseDBContext;

        public IEnumerable<Order> Orders => _skiHouseDBContext.Orders
            .Include(o=>o.Lines)
            .ThenInclude(l=>l.Product);

        public EFOrderRepository(SkiHouseDBContext skiHouseDBContext)
        {
            _skiHouseDBContext = skiHouseDBContext;
        }

        public void SaveOrder(Order order)
        {

            if (order.OrderId == 0)
            {
                _skiHouseDBContext.Orders.Add(order);
            }

            _skiHouseDBContext.SaveChanges();
        }
    }
}

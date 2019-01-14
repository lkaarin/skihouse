using SkiHouse.Web.Data.Models;
using System.Collections.Generic;

namespace SkiHouse.Web.Data.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> Orders { get; }
        void SaveOrder(Order order);
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkiHouse.Web.Data.Models;
using SkiHouse.Web.Data.Repositories.Interfaces;
using System.Linq;

namespace SkiHouse.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly Cart _cart;
        private readonly IOrderRepository _orderRepository;

        public OrderController(Cart cart, IOrderRepository orderRepository)
        {
            _cart = cart;
            _orderRepository = orderRepository;
        }

        public IActionResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if(_cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToList();
                _orderRepository.SaveOrder(order);
                _cart.Clear();

                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }

        public IActionResult Completed() => View();

        [Authorize]
        public IActionResult List() => View(_orderRepository.Orders.Where(o => !o.Shipped));

        [HttpPost]
        [Authorize]
        public IActionResult MarkShipped(int orderId)
        {
            var order = _orderRepository.Orders.FirstOrDefault(x => x.OrderId == orderId);
            if (order != null)
            {
                order.Shipped = true;
                _orderRepository.SaveOrder(order);
            }

            return RedirectToAction(nameof(List));
        }
    }
}
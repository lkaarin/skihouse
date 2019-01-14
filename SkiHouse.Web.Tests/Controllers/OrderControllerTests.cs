
using Microsoft.AspNetCore.Mvc;
using Moq;
using SkiHouse.Web.Controllers;
using SkiHouse.Web.Data.Models;
using SkiHouse.Web.Data.Repositories.Interfaces;
using Xunit;

namespace SkiHouse.Web.Tests.Controllers
{
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            OrderController target = new OrderController(cart, orderRepositoryMock.Object);

            var result = target.Checkout(new Order()) as ViewResult;

            orderRepositoryMock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Checkout_Invalid_Order()
        {
            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            OrderController target = new OrderController(cart, orderRepositoryMock.Object);
            target.ModelState.AddModelError("error", "error");

            var result = target.Checkout(new Order()) as ViewResult;

            orderRepositoryMock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Checkout_And_Submit_Order()
        {
            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            OrderController target = new OrderController(cart, orderRepositoryMock.Object);

            var result = target.Checkout(new Order()) as RedirectToActionResult;

            orderRepositoryMock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);
            Assert.Equal(nameof(OrderController.Completed), result.ActionName);
        }
    }
}

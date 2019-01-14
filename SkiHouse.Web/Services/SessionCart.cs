using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SkiHouse.Web.Data.Models;
using SkiHouse.Web.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkiHouse.Web.Services
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider services)
        {
            var session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;

            return cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session.SetJson("Cart", this);
        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            Session.SetJson("Cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace SkiHouse.Web.Data.Models
{
    public class Cart
    {
        private readonly List<CartLine> _cartLines = new List<CartLine>();

        public virtual void AddItem(Product product, int quantity)
        {
            var cartLine = _cartLines.FirstOrDefault(x => x.Product.ProductId == product.ProductId);

            if (cartLine == null)
            {
                _cartLines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                cartLine.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product) => _cartLines.RemoveAll(x => x.Product.ProductId == product.ProductId);

        public virtual void Clear() => _cartLines.Clear();

        public virtual decimal ComputeTotal() => _cartLines.Sum(x => x.Product.Price * x.Quantity);

        public virtual IEnumerable<CartLine> Lines => _cartLines;
    }
}

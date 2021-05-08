using System.Linq;
using Kata.Domain.Checkout;
using Xunit;

namespace Kata.Domain.UnitTests.Checkout
{
    public class BasketTests
    {
        [Fact]
        public void Basket_NewBasket_HasNoItems()
        {
            var basket = Basket.Create();
            Assert.Empty(basket.GetItems());
        }
    }
}
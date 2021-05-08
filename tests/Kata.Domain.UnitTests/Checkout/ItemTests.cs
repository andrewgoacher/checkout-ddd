using Kata.Domain.Checkout;
using Kata.Domain.Shared;
using Xunit;

namespace Kata.Domain.UnitTests.Checkout
{
    public class ItemTests
    {
        [Fact]
        public void NewItem_HasCorrectValues()
        {
            var basket = Basket.Create();
            var itemId = new ItemId("Item Id");
            var price = new Price(100);

            var item = Item.NewItem(basket, itemId, price);
            
            Assert.Equal(basket.Id, item.ParentId);
            Assert.Equal(itemId, item.Id);
            Assert.Equal(price, item.Price);
        }
    }
}
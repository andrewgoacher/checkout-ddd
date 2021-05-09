using Kata.Domain.Checkout;
using Kata.Domain.Shared;
using Kata.Domain.UnitTests.Services;
using Xunit;

namespace Kata.Domain.UnitTests.Checkout
{
    public class ItemTests
    {
        [Fact]
        public void NewItem_HasCorrectValues()
        {
            var basket = Basket.Create(new ItemServiceStub());
            var itemId = new ItemId("Item Id");
            var price = new Price(100);
            var quantity = new Quantity(1);

            var item = Item.NewItem(basket, itemId, price, quantity);
            
            Assert.Equal(basket.Id, item.ParentId);
            Assert.Equal(itemId, item.Id);
            Assert.Equal(price, item.Price);
            Assert.Equal(quantity, item.Quantity);
        }

        [Fact]
        public void NewItem_NoQuantity_ThrowsInvalidItemQuantityException()
        {
            var basket = Basket.Create(new ItemServiceStub());
            var itemId = new ItemId("Item Id");
            var price = new Price(100);
            var quantity = new Quantity(0);

            Assert.Throws<InvalidItemQuantityException>(() =>
            {
                Item.NewItem(basket, itemId, price, quantity);
            });
        }
    }
}
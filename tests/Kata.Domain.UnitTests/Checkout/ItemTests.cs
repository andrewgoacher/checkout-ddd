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

            var item = Item.NewItem(basket.Id, itemId, price, quantity);

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
                Item.NewItem(basket.Id, itemId, price, quantity);
            });
        }

        [Fact]
        public void Item_IncrementQuantity_UpdatesItemQuantity()
        {
            var basket = Basket.Create(new ItemServiceStub());
            var itemId = new ItemId("Item Id");
            var price = new Price(100);
            var quantity = new Quantity(1);

            var item = Item.NewItem(basket.Id, itemId, price, quantity);
            item.IncrementQuantity(quantity);

            Assert.Equal(2, item.Quantity);
        }

        [Fact]
        public void Item_DecrementQuantity_UpdatesItemQuantity()
        {
            var basket = Basket.Create(new ItemServiceStub());
            var itemId = new ItemId("Item Id");
            var price = new Price(100);
            var quantity = new Quantity(1);

            var item = Item.NewItem(basket.Id, itemId, price, quantity);
            item.DecrementQuantity(quantity);

            Assert.Equal(0, item.Quantity);
        }
    }
}
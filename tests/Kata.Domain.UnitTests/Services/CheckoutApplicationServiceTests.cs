using System;
using System.Linq;
using System.Threading.Tasks;
using Kata.Domain.Checkout;
using Kata.Domain.Services;
using Xunit;

namespace Kata.Domain.UnitTests.Services
{
    public class CheckoutApplicationServiceTests
    {
        public CheckoutApplicationServiceTests()
        {
        }

        [Fact]
        public async Task CheckoutApplicationService_CreateBasket_ReturnsValidBasketId()
        {
            var service = new CheckoutApplicationService(new ItemServiceStub(), new BasketStoreFake());

            var basketId = await service.CreateBasket();

            Assert.NotNull(basketId);
        }

        [Fact]
        public async Task CheckoutApplicationService_AddItem_BasketNotFound_ThrowsBasketNotFoundException()
        {
            var service = new CheckoutApplicationService(new ItemServiceStub(), new BasketStoreFake());

            await Assert.ThrowsAsync<BasketNotFoundException>(async () =>
            {
                await service.AddItemAsync(new BasketId(Guid.NewGuid()), new("A"), new(1));
            });
        }

        [Fact]
        public async Task CheckoutApplicationService_AddItem_AddsItem()
        {
            var store = new BasketStoreFake();
            var service = new CheckoutApplicationService(new ItemServiceStub(), store);
            var basketId = await service.CreateBasket();

            await service.AddItemAsync(basketId, new("A"), new(1));

            var basket = await store.GetBasketAsync(basketId);
            var items = basket.GetItems();
            Assert.NotNull(items);
        }

        [Fact]
        public async Task CheckoutApplicationService_RemoveItem_BasketNotFound_ThrowsBasketNotFoundException()
        {
            var service = new CheckoutApplicationService(new ItemServiceStub(), new BasketStoreFake());

            await Assert.ThrowsAsync<BasketNotFoundException>(async () =>
            {
                await service.RemoveItemAsync(new BasketId(Guid.NewGuid()), new("A"), new(1));
            });
        }

        [Fact]
        public async Task CheckoutApplicationService_RemoveItem_RemovesQuantity()
        {
            var store = new BasketStoreFake();
            var service = new CheckoutApplicationService(new ItemServiceStub(), store);
            var basketId = await service.CreateBasket();

            await service.AddItemAsync(basketId, new("A"), new(3));
            await service.RemoveItemAsync(basketId, new("A"), new(1));

            var basket = await store.GetBasketAsync(basketId);
            var items = basket.GetItems();

            var item = items.Single();
            Assert.Equal(2, item.Quantity);
        }
    }
}

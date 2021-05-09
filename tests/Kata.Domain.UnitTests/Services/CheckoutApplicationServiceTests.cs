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
            var service = new CheckoutApplicationService(new ItemServiceStub(), new BasketStoreFake(), new DiscountRuleService());

            var basketId = await service.CreateBasket();

            Assert.NotNull(basketId);
        }

        [Fact]
        public async Task CheckoutApplicationService_AddItem_BasketNotFound_ThrowsBasketNotFoundException()
        {
            var service = new CheckoutApplicationService(new ItemServiceStub(), new BasketStoreFake(), new DiscountRuleService());

            await Assert.ThrowsAsync<BasketNotFoundException>(async () =>
            {
                await service.AddItemAsync(new BasketId(Guid.NewGuid()), new("A"), new(1));
            });
        }

        [Fact]
        public async Task CheckoutApplicationService_AddItem_AddsItem()
        {
            var store = new BasketStoreFake();
            var service = new CheckoutApplicationService(new ItemServiceStub(), store, new DiscountRuleService());
            var basketId = await service.CreateBasket();

            await service.AddItemAsync(basketId, new("A"), new(1));

            var basket = await store.GetBasketAsync(basketId);
            var items = basket.GetItems();
            Assert.NotNull(items);
        }

        [Fact]
        public async Task CheckoutApplicationService_RemoveItem_BasketNotFound_ThrowsBasketNotFoundException()
        {
            var service = new CheckoutApplicationService(new ItemServiceStub(), new BasketStoreFake(), new DiscountRuleService());

            await Assert.ThrowsAsync<BasketNotFoundException>(async () =>
            {
                await service.RemoveItemAsync(new BasketId(Guid.NewGuid()), new("A"), new(1));
            });
        }

        [Fact]
        public async Task CheckoutApplicationService_RemoveItem_RemovesQuantity()
        {
            var store = new BasketStoreFake();
            var service = new CheckoutApplicationService(new ItemServiceStub(), store, new DiscountRuleService());
            var basketId = await service.CreateBasket();

            await service.AddItemAsync(basketId, new("A"), new(3));
            await service.RemoveItemAsync(basketId, new("A"), new(1));

            var basket = await store.GetBasketAsync(basketId);
            var items = basket.GetItems();

            var item = items.Single();
            Assert.Equal(2, item.Quantity);
        }

        [Theory]
        [InlineData("A", 3, 130)]
        [InlineData("A", 6, 260)]
        [InlineData("B", 2, 45)]
        [InlineData("B", 4, 90)]
        public async Task CheckoutApplicationService_AddItems_UpdatesTotalWithAppliedDiscounts(string itemId, int count, decimal expectedOutput)
        {
            var store = new BasketStoreFake();
            var service = new CheckoutApplicationService(new ItemServiceStub(), store, new DiscountRuleService());
            var basketId = await service.CreateBasket();

            await service.AddItemAsync(basketId, new(itemId), new(count));

            var basket = await store.GetBasketAsync(basketId);
            Assert.Equal(expectedOutput, basket.GetTotal());
        }
    }
}

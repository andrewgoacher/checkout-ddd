using System;
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
    }
}

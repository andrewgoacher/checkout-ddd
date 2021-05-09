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
        public void CheckoutApplicationService_CreateBasket_ReturnsValidBasketId()
        {
            var service = new CheckoutApplicationService(new ItemServiceStub());

            var basketId = service.CreateBasket();

            Assert.NotNull(basketId);
        }

        [Fact]
        public void CheckoutApplicationService_BaksetAlreadyExists_ThrowsBasketExistsException()
        {
            var service = new CheckoutApplicationService(new ItemServiceStub());

            service.CreateBasket();

            Assert.Throws<BasketExistsException>(() =>
            {
                service.CreateBasket();
            });
        }
    }
}

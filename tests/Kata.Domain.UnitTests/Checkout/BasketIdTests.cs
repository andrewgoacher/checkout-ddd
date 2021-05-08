using System;
using Kata.Domain.Checkout;
using Xunit;

namespace Kata.Domain.UnitTests.Checkout
{
    public class BasketIdTests
    {
        [Fact]
        public void BasketId_InvalidGuid_ThrowsInvalidBasketIdException()
        {
            Assert.Throws<InvalidBasketIdException>(() =>
            {
                new BasketId(Guid.Empty);
            });
        }

        [Fact]
        public void BasketId_CorrectGuid_HasSameGuid()
        {
            var id = Guid.NewGuid();

            var basketId = new BasketId(id);
            
            Assert.Equal(id, basketId);
        }

        [Fact]
        public void BasketId_SameId_SameBasketId()
        {
            var id = Guid.NewGuid();

            var basket1 = new BasketId(id);
            var basket2 = new BasketId(id);
            
            Assert.Equal(basket1, basket2);
        }

        [Fact]
        public void BasketId_DifferentId_DifferentBasketIds()
        {
            var basket1 = new BasketId(Guid.NewGuid());
            var basket2 = new BasketId(Guid.NewGuid());
            
            Assert.NotEqual(basket1, basket2);
        }
    }
}
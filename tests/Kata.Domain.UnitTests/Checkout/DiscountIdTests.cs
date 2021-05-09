using System;
using Kata.Domain.Checkout;
using Xunit;

namespace Kata.Domain.UnitTests.Checkout
{
    public class DiscountIdTests
    {
        [Fact]
        public void DiscountId_NullString_ThrowsInvalidDiscountIdException()
        {
            Assert.Throws<InvalidDiscountIdException>(() =>
            {
                new DiscountId(default);
            });
        }

        [Fact]
        public void DiscountId_ValidId_ContainsCorrectId()
        {
            var id = Guid.NewGuid();
            var discountId = new DiscountId(id);
            
            Assert.Equal(id, discountId);
        }

        [Fact]
        public void DiscountId_SameId_AreEqual()
        {
            var id = Guid.NewGuid();

            var id1 = new DiscountId(id);
            var id2 = new DiscountId(id);
            
            Assert.Equal(id1, id2);
        }

        [Fact]
        public void DiscountId_DifferentId_AreNotEqual()
        {
            var id1 = new DiscountId(Guid.NewGuid());
            var id2 = new DiscountId(Guid.NewGuid());
            
            Assert.NotEqual(id1, id2);
        }
    }
}
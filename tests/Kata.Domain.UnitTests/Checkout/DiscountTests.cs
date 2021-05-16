using System;
using Kata.Domain.Checkout;
using Kata.Domain.Shared;
using Kata.Domain.UnitTests.Services;
using Xunit;

namespace Kata.Domain.UnitTests.Checkout
{
    public class DiscountTests
    {
        public DiscountTests()
        {
        }

        [Fact]
        public void NewDiscount_HasCorrectValues()
        {
            var basket = Basket.Create(new ItemServiceStub());
            var id = new DiscountId(Guid.NewGuid());
            var desc = new Description("Test description");
            var amount = new Money(100);

            var discount = Discount.NewDiscount(basket.Id, id, desc, amount);

            Assert.Equal(basket.Id, discount.ParentId);
            Assert.Equal(id, discount.Id);
            Assert.Equal(desc, discount.Description);
            Assert.Equal(amount, discount.Amount);
        }
    }
}

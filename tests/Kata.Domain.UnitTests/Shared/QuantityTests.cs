using Kata.Domain.Shared;
using Xunit;

namespace Kata.Domain.UnitTests.Shared
{
    public class QuantityTests
    {
        public QuantityTests()
        {
        }

        [Fact]
        public void Quantity_BelowZero_ThrowsInvalidQuantityException()
        {
            Assert.Throws<InvalidQuantityException>(() =>
            {
                new Quantity(-1);
            });
        }

        [Fact]
        public void Quantity_Created_HasCorrectValue()
        {
            var value = 5;
            var quantity = new Quantity(value);

            Assert.Equal(value, quantity);
        }

        [Fact]
        public void Quantity_Increment_DoesNotChangeOriginal()
        {
            var originalQuantity = new Quantity(0);
            originalQuantity.Increment();

            Assert.Equal(0, originalQuantity);
        }

        [Fact]
        public void Quantity_Increment_Returns1More()
        {
            var originalQuantity = new Quantity(0);
            var newQuantity = originalQuantity.Increment();

            Assert.Equal(1, newQuantity);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Quantity_IncrementBy_ReturnsAmountMore(int increment)
        {
            var original = new Quantity(10);
            var newQuantity = original.IncrementBy(new Quantity(increment));

            Assert.Equal(10 + increment, newQuantity);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Quantity_DecrementBy_ReturnsAmountLess(int decrement)
        {
            var original = new Quantity(10);
            var newQuantity = original.DecrementBy(new Quantity(decrement));

            Assert.Equal(10 - decrement, newQuantity);
        }
    }
}

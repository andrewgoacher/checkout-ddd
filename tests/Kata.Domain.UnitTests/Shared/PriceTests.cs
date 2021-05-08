using Kata.Domain.Shared;
using Xunit;

namespace Kata.Domain.UnitTests.Shared
{
    public class PriceTests
    {
        [Theory]
        [InlineData(-12.12)]
        [InlineData(0)]
        public void Price_LessThanOrZero_ThrowsInvalidPriceException(decimal value)
        {
            Assert.Throws<InvalidPriceException>(() =>
            {
                new Price(value);
            });

            var price1 = new Price(1);
            var price2 = new Price(2);
        }
    }
}
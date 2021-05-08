using Kata.Domain.Shared;
using Xunit;

namespace Kata.Domain.UnitTests.Shared
{
    public class MoneyTests
    {
        [Theory]
        [InlineData(1.234)]
        [InlineData(1.2345)]
        [InlineData(1.23456)]
        [InlineData(-1.234)]
        public void Money_WithMoreThan2DecimalPlaces_ThrowsTooManyDecimalPlacesException(decimal input)
        {
            Assert.Throws<TooManyDecimalPlacesException>(() =>
            {
                new Money(input);
            });
        }
        
        [Theory]
        [InlineData(1.23)]
        [InlineData(-1.23)]
        [InlineData(123.34)]
        [InlineData(100)]
        public void Money_CorrectDecimalPlaces_HasCorrectValue(decimal input)
        {
            var money = new Money(input);
            Assert.Equal(input, money);
        }

        [Fact]
        public void Money_SameValue_AreEqual()
        {
            Assert.Equal(new Money(5), new Money(5));
        }

        [Fact]
        public void Money_DifferentValue_AreNotEqual()
        {
            Assert.NotEqual(new Money(5), new Money(6));
        }

        [Fact]
        public void Money_Addition_ShouldReturnCorrectValue()
        {
            var moneyA = new Money(5);
            var moneyB = new Money(6);
            
            Assert.Equal(11m, (moneyA + moneyB));
        }

        [Fact]
        public void Money_Addition_ShouldNotModifyOriginalValues()
        {
            var moneyA = new Money(5);
            var moneyB = new Money(6);

            var moneyC = moneyA + moneyB;

            Assert.Equal(5m, moneyA);
            Assert.Equal(6m, moneyB);
        }

        [Fact]
        public void Money_Subtraction_ShouldReturnCorrectValue()
        {
            var moneyA = new Money(10);
            var moneyB = new Money(2);
            
            Assert.Equal(8m, moneyA - moneyB);
        }

        [Fact]
        public void Money_Subtraction_ShouldNotModifyOriginalValues()
        {
            var moneyA = new Money(10);
            var moneyB = new Money(2);
            var moneyC = moneyA - moneyB;
            
            Assert.Equal(10m, moneyA);
            Assert.Equal(2m, moneyB);
        }
    }
}
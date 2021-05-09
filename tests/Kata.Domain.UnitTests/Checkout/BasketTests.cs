using System.Linq;
using System.Threading.Tasks;
using Kata.Domain.Checkout;
using Kata.Domain.UnitTests.Services;
using Xunit;

namespace Kata.Domain.UnitTests.Checkout
{
    public class BasketTests
    {
        [Fact]
        public void Basket_NewBasket_HasNoItems()
        {
            var basket = Basket.Create(new ItemServiceStub());
            Assert.Empty(basket.GetItems());
        }

        [Fact]
        public async Task Basket_AddItem_ContainsItem()
        {
            var basket = Basket.Create(new ItemServiceStub());
            await basket.AddItemAsync(new ItemId("A"), new(1));

            Assert.NotEmpty(basket.GetItems());
        }

        [Fact]
        public async Task Basket_AddItem_HasCorrectInformation()
        {
            var itemId = new ItemId("A");

            var serviceStub = new ItemServiceStub();
            var basket = Basket.Create(new ItemServiceStub());
            await basket.AddItemAsync(itemId, new(1));

            var expectedItem = await serviceStub.FetchItemAsync(itemId);
            var actualItem = basket.GetItems().Single();

            Assert.Equal(expectedItem.Id, actualItem.Id);
            Assert.Equal(expectedItem.Price, actualItem.Price);
        }

        [Fact]
        public async Task Basket_AddItem_HasCorretTotal()
        {
            var basket = Basket.Create(new ItemServiceStub());
            await basket.AddItemAsync(new ItemId("A"), new(1));

            Assert.Equal(50m, basket.GetTotal());
        }

        [Fact]
        public async Task Basket_AddItems_HasCorrectTotal()
        {
            var basket = Basket.Create(new ItemServiceStub());
            await basket.AddItemAsync(new ItemId("A"), new(1));
            await basket.AddItemAsync(new ItemId("B"), new(1));
            await basket.AddItemAsync(new ItemId("C"), new(1));
            await basket.AddItemAsync(new ItemId("D"), new(1));

            Assert.Equal(115m, basket.GetTotal());
        }
    }
}
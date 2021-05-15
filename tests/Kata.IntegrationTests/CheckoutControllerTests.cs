using System;
using System.Net.Http;
using System.Threading.Tasks;
using Kata.IntegrationTests.Fixtures;
using Xunit;

namespace Kata.IntegrationTests
{
    public class CheckoutControllerTests
        : IClassFixture<TestServerFixture>
    {
        private readonly HttpClient _client;

        public CheckoutControllerTests(TestServerFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task CreateBasketReturnsBasketId()
        {
            // Act
            var response = await _client.PostAsync("/api/checkout/", null);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var id = System.Text.Json.JsonSerializer.Deserialize<Guid>(responseString);

            Assert.NotEqual(Guid.Empty, id);
        }
    }
}

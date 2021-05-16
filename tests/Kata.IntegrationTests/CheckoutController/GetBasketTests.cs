using System;
using System.Net.Http;
using System.Threading.Tasks;
using Kata.Domain.Checkout;
using Kata.IntegrationTests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Kata.IntegrationTests.CheckoutController
{
    [Collection("Integration Tests")]
    public class GetBasketTests : IDisposable
    {
        private readonly HttpClient _client;
        private bool disposedValue;

        public GetBasketTests(TestServerFixture fixture)
        {
            _client = fixture.TestServer.CreateClient();
        }


        [Fact]
        public async Task GetBasket_InvalidRouteParam_Returns_BadRequest()
        {
            const string INVALID_BASKET_ID = "invalid basket id";
            using (var response = await _client.GetAsync($"/api/checkout/{INVALID_BASKET_ID}"))
            {
                Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var error = System.Text.Json.JsonSerializer.Deserialize<ProblemDetails>(responseString);

                Assert.NotNull(error);
            }
        }

        [Fact]
        public async Task GetBasket_InvalidId_Returns_BadRequest()
        {
            var INVALID_BASKET_ID = Guid.Empty.ToString();
            using (var response = await _client.GetAsync($"/api/checkout/{INVALID_BASKET_ID}"))
            {
                Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var error = System.Text.Json.JsonSerializer.Deserialize<ProblemDetails>(responseString);

                Assert.NotNull(error);
                Assert.Equal("Validation Error", error.Title);
                Assert.Equal("https://httpstatuses.com/400", error.Type);
                Assert.Equal(400, error.Status);
                Assert.EndsWith($"/api/checkout/{INVALID_BASKET_ID}", error.Instance);
            }
        }

        [Fact]
        public async Task GetBasket_ValidId_MissingId_Returns_NotFound()
        {
            var id = Guid.NewGuid().ToString();
            using (var response = await _client.GetAsync($"/api/checkout/{id}"))
            {
                Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var error = System.Text.Json.JsonSerializer.Deserialize<ProblemDetails>(responseString);

                Assert.NotNull(error);
                Assert.Equal("Not Found", error.Title);
                Assert.Equal("https://httpstatuses.com/404", error.Type);
                Assert.Equal(404, error.Status);
                Assert.EndsWith($"/api/checkout/{id}", error.Instance);
            }
        }

        [Fact]
        public async Task GetBasket_ValidId_Exists_ReturnBasket()
        {
            Guid id;

            using (var response = await _client.PostAsync($"/api/checkout/", null))
            {
                Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                id = System.Text.Json.JsonSerializer.Deserialize<Guid>(responseString);
            }

            using (var response = await _client.GetAsync($"/api/checkout/{id}"))
            {
                Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var basket = System.Text.Json.JsonSerializer.Deserialize<Basket>(responseString);

                Assert.NotNull(basket);
                Assert.Equal(new BasketId(id), basket.Id);
                Assert.Empty(basket.GetItems());
                Assert.Equal(0m, basket.GetTotal());
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _client?.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

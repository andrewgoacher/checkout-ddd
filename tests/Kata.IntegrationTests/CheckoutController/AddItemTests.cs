using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Kata.Domain.Checkout;
using Kata.Domain.Infrastructure.Serialisation;
using Kata.Domain.Services;
using Kata.IntegrationTests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Kata.IntegrationTests.CheckoutController
{
    [Collection("Integration Tests")]
    public class AddItemTests : IDisposable
    {
        private readonly HttpClient _client;
        private bool disposedValue;

        public AddItemTests(TestServerFixture fixture)
        {
            _client = fixture.TestServer.CreateClient();
        }

        [Fact]
        public async Task AddItem_EmptyItem_ReturnsBadRequest()
        {
            var basketId = await CreateBasket();

            var jsonContent = JsonContent.Create(new
            {
                ItemId = "",
                Quantity = 1
            });

            using (var response = await _client.PostAsync($"/api/checkout/{basketId}/addItem", jsonContent))
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var error = System.Text.Json.JsonSerializer.Deserialize<ProblemDetails>(responseString);

                Assert.NotNull(error);
                Assert.Equal("Validation Error", error.Title);
                Assert.Equal("https://httpstatuses.com/400", error.Type);
                Assert.Equal(400, error.Status);
                Assert.EndsWith($"/api/checkout/{basketId}/addItem", error.Instance);
            }
        }

        [Fact]
        public async Task AddItem_NoQuantityReturnsBadRequest()
        {
            var basketId = await CreateBasket();

            var jsonContent = JsonContent.Create(new
            {
                ItemId = "A",
                Quantity = 0
            });

            using (var response = await _client.PostAsync($"/api/checkout/{basketId}/addItem", jsonContent))
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var error = System.Text.Json.JsonSerializer.Deserialize<ProblemDetails>(responseString);

                Assert.NotNull(error);
                Assert.Equal("Validation Error", error.Title);
                Assert.Equal("https://httpstatuses.com/400", error.Type);
                Assert.Equal(400, error.Status);
                Assert.EndsWith($"/api/checkout/{basketId}/addItem", error.Instance);
            }
        }

        [Fact]
        public async Task AddItem_ItemNotFound_ReturnsNotFound()
        {
            var basketId = await CreateBasket();

            var jsonContent = JsonContent.Create(new
            {
                ItemId = "ItemNotHere",
                Quantity = 1
            });

            using (var response = await _client.PostAsync($"/api/checkout/{basketId}/addItem", jsonContent))
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var error = System.Text.Json.JsonSerializer.Deserialize<ProblemDetails>(responseString);

                Assert.NotNull(error);
                Assert.Equal("Not Found", error.Title);
                Assert.Equal("https://httpstatuses.com/404", error.Type);
                Assert.Equal(404, error.Status);
                Assert.EndsWith($"/api/checkout/{basketId}/addItem", error.Instance);
            }
        }

        [Fact]
        public async Task AddItem_UpdatesBasketState()
        {
            var basketId = await CreateBasket();

            var jsonContent = JsonContent.Create(new
            {
                ItemId = "A",
                Quantity = 1
            });

            using (var response = await _client.PostAsync($"/api/checkout/{basketId}/addItem", jsonContent))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }

            var basket = await GetBasket(basketId);

            Assert.Single(basket.GetItems());
            Assert.Equal(1, basket.GetItems().Single().Quantity);

            Assert.NotEqual(0m, basket.GetTotal());
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

        private async Task<Guid> CreateBasket()
        {
            using (var response = await _client.PostAsync("/api/checkout", null))
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return System.Text.Json.JsonSerializer.Deserialize<Guid>(responseString);
            }
        }

        private async Task<Basket> GetBasket(Guid id)
        {
            using (var response = await _client.GetAsync($"/api/checkout/{id}"))
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return System.Text.Json.JsonSerializer.Deserialize<Basket>(responseString, CustomSerialisationOptions.Get());
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

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Kata.IntegrationTests.Fixtures;
using Xunit;

namespace Kata.IntegrationTests.CheckoutController
{
    [Collection("Integration Tests")]
    public class CreateBasketTests : IDisposable
    {
        private readonly HttpClient _client;
        private bool disposedValue;

        public CreateBasketTests(TestServerFixture fixture)
        {
            _client = fixture.TestServer.CreateClient();
        }

        [Fact]
        public async Task CreateBasketReturnsBasketId()
        {
            using (var response = await _client.PostAsync("/api/checkout/", null))
            {
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var id = System.Text.Json.JsonSerializer.Deserialize<Guid>(responseString);

                Assert.NotEqual(Guid.Empty, id);
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

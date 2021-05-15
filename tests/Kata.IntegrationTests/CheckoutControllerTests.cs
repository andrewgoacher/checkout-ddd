using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Kata.Domain.Checkout;
using KataApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Kata.IntegrationTests
{
    public class CheckoutControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public CheckoutControllerTests()
        {
            string codeBase = Assembly.GetExecutingAssembly().Location;
            var dir =  Path.GetDirectoryName(codeBase);

            _server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseConfiguration(new ConfigurationBuilder()
                    .SetBasePath(dir)
                    .AddJsonFile("appsettings.json")
                    .Build()
                ) 
                .UseStartup<Startup>());

            _client = _server.CreateClient();
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



            var basketId = new BasketId(id);
        }
    }
}

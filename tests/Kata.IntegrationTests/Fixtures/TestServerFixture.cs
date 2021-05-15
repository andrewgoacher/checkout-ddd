using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using KataApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Kata.IntegrationTests.Fixtures
{
    public class TestServerFixture : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        private bool disposedValue;

        public TestServerFixture()
        {
            _server = new TestServer(new WebHostBuilder()
             .UseEnvironment("Development")
             .UseConfiguration(new ConfigurationBuilder()
                 .SetBasePath(GetTestBaseDir())
                 .AddJsonFile("appsettings.json")
                 .Build()
             )
             .UseStartup<Startup>());

            _client = _server.CreateClient();
        }

        public TestServer TestServer => _server;
        public HttpClient Client => _client;

        private static string GetTestBaseDir() =>
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _client?.Dispose();
                    _server?.Dispose();
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

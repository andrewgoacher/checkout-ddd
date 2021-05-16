using System;
using System.IO;
using System.Reflection;
using KataApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Kata.IntegrationTests.Fixtures
{
    [CollectionDefinition("Integration Tests")]
    public class TestServerFixtureCollection : ICollectionFixture<TestServerFixture>
    {

    }

    public class TestServerFixture : IDisposable
    {
        private readonly TestServer _server;

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
        }

        public TestServer TestServer => _server;

        public T GetService<T>() => (T)_server.Services.GetService(typeof(T));

        private static string GetTestBaseDir() =>
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
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

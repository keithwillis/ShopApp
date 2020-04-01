using Xunit;
using System;
using Microsoft.AspNetCore.Hosting;

namespace SampleShop.Tests.Integration
{
    /// <summary>
    /// A base for integration tests which launches a self-hosted HTTP server
    /// </summary>
    public class BaseIntegrationTest : IDisposable
    {
        private IWebHost host;
        private static int hostPort = 9443;

        public BaseIntegrationTest()
        {
            host = Program.CreateWebHostBuilder(null).UseUrls($"http://*:{hostPort}").Build();
            host.Start();
        }

        public Uri ApiAction(string actionName)
        {
            return new Uri(string.Format("http://localhost:{0}/api/{1}", hostPort, actionName));
        }

        public void Dispose()
        {
            host.StopAsync();
        }
    }
}
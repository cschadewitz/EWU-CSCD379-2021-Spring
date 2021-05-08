using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SecretSanta.Web;
using SecretSanta.Web.Api;
using SecretSanta.Web.Tests.Api;

namespace SecretSanta.Web.Tests
{
    internal class WebApplicationFactory : WebApplicationFactory<Startup>
    {
        internal TestableUsersClient Client { get; } = new();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => {
                services.AddScoped<IUsersClient, TestableUsersClient>(_ => Client);
            });
        }
    }
}

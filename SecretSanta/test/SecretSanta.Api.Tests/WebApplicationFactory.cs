using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SecretSanta.Api.Tests.Business;
using SecretSanta.Business;

namespace SecretSanta.Api.Tests
{
    internal class WebApplicationFactory : WebApplicationFactory<Startup>
    {
        internal TestableUserRepository UserRepository { get; } = new();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => {
                services.AddScoped<IUserRepository, TestableUserRepository>(_ => UserRepository);
            });
        }
    }
}

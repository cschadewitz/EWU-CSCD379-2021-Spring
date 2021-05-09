using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SecretSanta.Web;
using SecretSanta.Web.Api;

namespace SecretSanta.Web.SystemTests
{
    internal class WebApplicationFactory : WebApplicationFactory<Startup>
    {
        public IUsersClient Client { get; set; }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
        }
    }
}

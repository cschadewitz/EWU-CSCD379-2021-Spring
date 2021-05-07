using System;
using System.Collections.Generic;
using System.Net.Http;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SecretSanta.Web;
using SecretSanta.Web.Api;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Tests
{
    public class WebApplicationFactory : WebApplicationFactory<Startup>
    {
        public static HttpClient ApiClient = new()
        {
            BaseAddress = new Uri("https://localhost:5101")
        };
        public IUsersClient UsersClient { get; }
        public WebApplicationFactory(IUsersClient usersClient)
        {
            UsersClient = usersClient ?? throw new ArgumentNullException(nameof(usersClient));
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IUsersClient, IUsersClient>(_ => UsersClient);
                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<UserDTO, UserViewModel>();
                    cfg.CreateMap<UserViewModel, UserDTO>();
                });
                configuration.AssertConfigurationIsValid();
                configuration.CompileMappings();
                services.AddSingleton<IMapper>(new Mapper(configuration));
            });
        }
    }
}

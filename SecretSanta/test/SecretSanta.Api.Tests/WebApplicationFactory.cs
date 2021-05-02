using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SecretSanta.Api.DTO;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Tests
{
    public class WebApplicationFactory : WebApplicationFactory<Startup>
    {

        Dictionary<Type, IEntityRepository> EntityRepositories;
        public WebApplicationFactory(Dictionary<Type,IEntityRepository> requiredEntityRepositories)
        {

            EntityRepositories = requiredEntityRepositories ?? throw new ArgumentNullException(nameof(requiredEntityRepositories));
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                foreach(KeyValuePair<Type, IEntityRepository> repo in EntityRepositories)
                {
                    services.AddSingleton(repo.Key, repo.Value);
                }
                
                services.AddAutoMapper(typeof(MappingProfileApi));
            });
        }
    }
}

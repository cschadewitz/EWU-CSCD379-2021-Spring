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
        public Mock<IUserRepository> MockUserRepository { get; } = new();
        public Mock<IMapper> MockMapper { get; } = new();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            ConfigureMocks();
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IUserRepository>(_ => MockUserRepository.Object);
                services.AddAutoMapper(typeof(MappingProfileApi));
            });
        }

        private void ConfigureMocks()
        {
            User mockUser = new User { Id = 1, FirstName = "test", LastName = "user" };
            List<User> mockUsers = new List<User>
            {
                mockUser
            };
            MockUserRepository.Setup(r => r.List()).Returns((ICollection<User>)mockUsers);
            MockUserRepository.Setup(r => r.GetItem(1)).Returns(mockUser);
            MockUserRepository.Setup(r => r.GetItem(2)).Returns((User?)null);
            MockUserRepository.Setup(r => r.Remove(1)).Returns(true);
            MockUserRepository.Setup(r => r.Remove(2)).Returns(false);
        }
    }
}

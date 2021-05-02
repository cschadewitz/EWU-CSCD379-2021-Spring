using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.DTO;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void Constructor_GivenMissingUserRepository_ThrowsArgumentNullException()
        {
            WebApplicationFactory factory = new();
            ArgumentNullException ex = Assert.ThrowsException<ArgumentNullException>(
                () => new UsersController(null!,  factory.MockMapper.Object));
            Assert.AreEqual("userRepository", ex.ParamName);
        }
        [TestMethod]
        public void Constructor_GivenMissingMapper_ThrowsArgumentNullException()
        {
            WebApplicationFactory factory = new();
            ArgumentNullException ex = Assert.ThrowsException<ArgumentNullException>(
                () => new UsersController(factory.MockUserRepository.Object, null!));
            Assert.AreEqual("mapper", ex.ParamName);
        }
        [TestMethod]
        public async Task Get_GivenData_ReturnsUserDTOs()
        {
            WebApplicationFactory factory = new();
            HttpClient client = factory.CreateClient();

            HttpResponseMessage response = await client.GetAsync("/api/users/");
            IEnumerable<UserDTO>? userDTOs = await response.Content.ReadFromJsonAsync<IEnumerable<UserDTO>>();

            factory.MockUserRepository.Verify(r => r.List(), Times.AtLeastOnce());
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(factory.MockUserRepository.Object.List().Count, userDTOs.Count());
        }
        [TestMethod]
        public async Task Get_GivenValidId_ReturnsUser()
        {
            WebApplicationFactory factory = new();
            HttpClient client = factory.CreateClient();
            int id = 1;

            HttpResponseMessage response = await client.GetAsync($"/api/users/{id}");
            UserDTO? userDTO = await response.Content.ReadFromJsonAsync<UserDTO>();

            factory.MockUserRepository.Verify(r => r.GetItem(id), Times.AtLeastOnce());
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(factory.MockUserRepository.Object.GetItem(id).Id, userDTO.Id);
            Assert.AreEqual(factory.MockUserRepository.Object.GetItem(id).FirstName, userDTO.FirstName);
            Assert.AreEqual(factory.MockUserRepository.Object.GetItem(id).LastName, userDTO.LastName);
        }
        [TestMethod]
        public async Task Get_GivenInvalidId_ReturnsNotFound()
        {
            WebApplicationFactory factory = new();
            HttpClient client = factory.CreateClient();
            int id = 2;

            HttpResponseMessage response = await client.GetAsync($"/api/users/{id}");
            UserDTO? userDTO = await response.Content.ReadFromJsonAsync<UserDTO>();

            factory.MockUserRepository.Verify(r => r.GetItem(id), Times.AtLeastOnce());
            Assert.IsTrue(response.StatusCode is HttpStatusCode.NotFound);
        }
        [TestMethod]
        public async Task Delete_GivenValidId_ReturnsUser()
        {
            WebApplicationFactory factory = new();
            HttpClient client = factory.CreateClient();
            int id = 1;

            HttpResponseMessage response = await client.DeleteAsync($"/api/users/{id}");

            factory.MockUserRepository.Verify(r => r.Remove(id), Times.AtLeastOnce());
            response.EnsureSuccessStatusCode();
        }
        [TestMethod]
        public async Task Delete_GivenInvalidId_ReturnsNotFound()
        {
            WebApplicationFactory factory = new();
            HttpClient client = factory.CreateClient();
            int id = 2;

            HttpResponseMessage response = await client.DeleteAsync($"/api/users/{id}");

            factory.MockUserRepository.Verify(r => r.Remove(id), Times.AtLeastOnce());
            Assert.IsTrue(response.StatusCode is HttpStatusCode.NotFound);
        }

    }
}

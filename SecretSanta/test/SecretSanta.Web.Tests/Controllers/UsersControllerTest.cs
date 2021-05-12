using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Business;
using SecretSanta.Web.Controllers;
using SecretSanta.Web.Api;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTest
    {
        WebApplicationFactory Factory;
        HttpClient Client;
        public Mock<IUsersClient> MockUsersClient { get; } = new();
        public Mock<IMapper> MockMapper { get; } = new();

        private void Setup()
        {
            ConfigureMocks();
            Factory = new(MockUsersClient.Object);
            Client = Factory.CreateClient();
        }
        private void ConfigureMocks()
        {
            UserDTO mockUserDTO = new UserDTO { Id = 1, FirstName = "Test", LastName = "User" };
            List<UserDTO> mockUserDTOs = new List<UserDTO>
            {
                mockUserDTO
            };

            Task<ICollection<UserDTO>> getAllAsyncTask = Task<ICollection<UserDTO>>.Run(() =>
            {
                return (ICollection<UserDTO>)mockUserDTOs;
            });
            MockUsersClient.Setup(c => c.GetAllAsync()).Returns(getAllAsyncTask);

            Task<UserDTO> getAsyncValidTask = Task<UserDTO>.Run(() =>
            {
                return mockUserDTO;
            });
            Task<UserDTO> getAsyncInvalidTask = Task<UserDTO>.Run(() =>
            {
                return mockUserDTO;
            });
            MockUsersClient.Setup(c => c.GetAsync(1)).Returns(getAsyncValidTask);
            MockUsersClient.Setup(c => c.GetAsync(2)).Throws(new ApiException("A server side error occurred.", 404, "", null, null));

            Func<UserDTO, Task<UserDTO>> postAsyncValidFunc = (userDTO) => Task<UserDTO>.Run(() => userDTO);
            MockUsersClient.Setup(c => c.PostAsync(It.IsAny<UserDTO>())).Returns<UserDTO>((userDTO) => postAsyncValidFunc(userDTO));

            MockUsersClient.Setup(c => c.PutAsync(1, It.IsAny<UserDTO>())).Returns(Task.Run(() => { }));
            MockUsersClient.Setup(c => c.PutAsync(2, It.IsAny<UserDTO>())).Throws(new ApiException("A server side error occurred.", 404, "", null, null));

            MockUsersClient.Setup(c => c.DeleteAsync(1)).Returns(Task.Run(() => { }));
            MockUsersClient.Setup(c => c.DeleteAsync(2)).Throws(new ApiException("A server side error occurred.", 404, "", null, null));
        }

            //Unit Tests

            [TestMethod]
        public void Constructor_GivenMissingUserClientService_ThrowsArgumentNullException()
        {
            ArgumentNullException ex = Assert.ThrowsException<ArgumentNullException>(
                () => new UsersController(null!, MockMapper.Object));
            Assert.AreEqual("usersClient", ex.ParamName);
        }
        [TestMethod]
        public void Constructor_GivenMissingMapperService_ThrowsArgumentNullException()
        {
            ArgumentNullException ex = Assert.ThrowsException<ArgumentNullException>(
                () => new UsersController(MockUsersClient.Object, null!));
            Assert.AreEqual("mapper", ex.ParamName);
        }

        //

        [TestMethod]
        public async Task IndexGet_GivenData_ReturnsUsersIndexPage()
        {
            Setup();

            HttpResponseMessage response = await Client.GetAsync("/Users/");
            string content = await response.Content.ReadAsStringAsync();

            MockUsersClient.Verify(c => c.GetAllAsync(), Times.AtLeastOnce());
            response.EnsureSuccessStatusCode();
            Assert.IsTrue(content.Contains("<title>Secret Santa - Users</title>"));
        }
        [TestMethod]
        public async Task EditGet_GivenValidId_ReturnsUserEditPage()
        {
            Setup();
            int id = 1;

            HttpResponseMessage response = await Client.GetAsync($"/Users/Edit/{id}");
            string content = await response.Content.ReadAsStringAsync();

            MockUsersClient.Verify(c => c.GetAsync(id), Times.AtLeastOnce());
            response.EnsureSuccessStatusCode();
            Assert.IsTrue(content.Contains("<title>Secret Santa - Users - Edit</title>"));
            Assert.IsTrue(content.Contains($"id=\"Id\" name=\"Id\" value=\"{id}\""));
        }
        [TestMethod]
        public async Task CreateGet_GiveApiFunctional_ReturnsCreateUserPage()
        {
            Setup();

            HttpResponseMessage response = await Client.GetAsync("/Users/Create/");
            string content = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Assert.IsTrue(content.Contains("<title>Secret Santa - Users - Create</title>"));
        }
        [TestMethod]
        public async Task CreatePost_GiveValidUser_ReturnsUsersIndexPage()
        {
            Setup();
            Dictionary<string, string> userValues = new()
            {
                { nameof(UserViewModel.Id), "6" },
                { nameof(UserViewModel.FirstName), "Yet Another" },
                { nameof(UserViewModel.LastName), "Test User"}
            };
            FormUrlEncodedContent userContent = new(userValues!);

            HttpResponseMessage response = await Client.PostAsync("/Users/Create/", userContent);
            string content = await response.Content.ReadAsStringAsync();

            MockUsersClient.Verify(c => c.PostAsync(It.IsAny<UserDTO>()), Times.AtLeastOnce());
            response.EnsureSuccessStatusCode();
            Assert.IsTrue(content.Contains("<title>Secret Santa - Users</title>"));
        }
        [TestMethod]
        public async Task DeletePost_GiveValidId_ReturnsUsersIndexPage()
        {
            Setup();
            int id = 1;
;
            HttpResponseMessage response = await Client.DeleteAsync($"/Users/Delete/{id}");
            string content = await response.Content.ReadAsStringAsync();

            MockUsersClient.Verify(c => c.DeleteAsync(id), Times.AtLeastOnce());
            response.EnsureSuccessStatusCode();
            Assert.IsTrue(content.Contains("<title>Secret Santa - Users</title>"));
        }
    }
}

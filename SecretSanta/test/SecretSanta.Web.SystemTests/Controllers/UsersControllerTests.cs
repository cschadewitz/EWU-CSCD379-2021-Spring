using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Web.Api;
using SecretSanta.Web.Controllers;
using SecretSanta.Web.ViewModels;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace SecretSanta.Web.SystemTests.Controllers
{
    [TestClass]
    public class UsersControllerTests
    {
        HttpClient apiClient = new();
        HttpClient requestClient = new();
        [TestInitialize]
        public void Initialize()
        {
            if(apiClient.BaseAddress is null)
                apiClient.BaseAddress = new Uri("https://secretsantacasey-api.azurewebsites.net");
            if (requestClient.BaseAddress is null)
                requestClient.BaseAddress = new Uri("https://secretsantacasey.azurewebsites.net");
        }
        [TestMethod]
        public async Task Index_WithUsers_RetrievesUsers()
        {

            HttpResponseMessage response = await requestClient.GetAsync("/users/index");

            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public async Task Create_WithValidModel_CreatesUserAndReturnsToUsersIndex()
        {
            UsersClient usersClient = new(apiClient);
            int id = int.MaxValue;

            var values = new Dictionary<string?, string?>
            {
                { nameof(UserViewModel.Id), $"{id}" },
                { nameof(UserViewModel.FirstName), "John" },
                { nameof(UserViewModel.LastName), "Smith" },
            };
            using FormUrlEncodedContent content = new(values);

            HttpResponseMessage response = await requestClient.PostAsync("/users/create", content);
            string responseContent = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Web.Api.User? createdUser = await usersClient.GetAsync(id);
            Assert.AreEqual(id, createdUser.Id);
            Assert.AreEqual("John", createdUser.FirstName);
            Assert.AreEqual("Smith", createdUser.LastName);
            Assert.IsTrue(responseContent.Contains("<title>Secret Santa - Users</title>"));

            //await usersClient.DeleteAsync(id);
        }

        [TestMethod]
        public async Task Create_WithInvalidModel_DoesNotCreatesUserAndReturnsUsersCreatePage()
        {
            UsersClient usersClient = new(apiClient);
            int id = int.MaxValue;
            string userFirstName = "UserFirstName";
            var values = new Dictionary<string?, string?>
            {
                { nameof(UserViewModel.Id), id.ToString() },
                { nameof(UserViewModel.FirstName), userFirstName },
                { nameof(UserViewModel.LastName), "" }
            };
            using FormUrlEncodedContent content = new(values);

            HttpResponseMessage response = await requestClient.PostAsync("/users/create", content);
            string responseContent = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Assert.IsTrue(responseContent.Contains("<title>Secret Santa - Users - Create</title>"));
            Assert.IsTrue(responseContent.Contains($"id=\"FirstName\" name=\"FirstName\" value=\"{userFirstName}\""));
        }

        [TestMethod]
        public async Task Edit_WithUserId_RetrievesUser()
        {
            UsersClient usersClient = new(apiClient);
            int id = int.MaxValue;
            string userFirstName = "UserFirstName";
            await usersClient.PostAsync(new Web.Api.User
            {
                Id = id,
                FirstName = userFirstName,
                LastName = "Smith"
            });

            HttpResponseMessage response = await requestClient.GetAsync($"/users/edit/{id}");
            string responseContent = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Assert.IsTrue(responseContent.Contains("<title>Secret Santa - Users - Edit</title>"));
            Assert.IsTrue(responseContent.Contains($"id=\"FirstName\" name=\"FirstName\" value=\"{userFirstName}\""));

            await usersClient.DeleteAsync(id);
        }

        [TestMethod]
        public async Task Edit_WithValidModel_UpdatesUser()
        {
            UsersClient usersClient = new(apiClient);
            int id = int.MaxValue;
            string userFirstName = "UserFirstName";
            await usersClient.PostAsync(new Web.Api.User
            {
                Id = id,
                FirstName = "John",
                LastName = "Smith"
            });

            var values = new Dictionary<string?, string?>
            {
                { nameof(UserViewModel.Id), $"{id}" },
                { nameof(UserViewModel.FirstName), userFirstName },
                { nameof(UserViewModel.LastName), "Doe" },
            };
            using FormUrlEncodedContent content = new(values);

            HttpResponseMessage response = await requestClient.PostAsync("/users/edit", content);
            string responseContent = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Web.Api.User? createdUser = await usersClient.GetAsync(id);
            Assert.AreEqual(id, createdUser.Id);
            Assert.AreEqual(userFirstName, createdUser.FirstName);
            Assert.AreEqual("Doe", createdUser.LastName);
            Assert.IsTrue(responseContent.Contains("<title>Secret Santa - Users</title>"));

            await usersClient.DeleteAsync(id);
        }

        [TestMethod]
        public async Task Edit_WithInvalidModel_DoesNotUpdateUser()
        {
            UsersClient usersClient = new(apiClient);
            int id = int.MaxValue;
            string userFirstName = "UserFirstName";
            await usersClient.PostAsync(new Web.Api.User
            {
                Id = id,
                FirstName = "John",
                LastName = "Smith"
            });

            var values = new Dictionary<string?, string?>
            {
                { nameof(UserViewModel.Id), id.ToString() },
                { nameof(UserViewModel.FirstName), userFirstName },
                { nameof(UserViewModel.LastName), "" },
            };
            using FormUrlEncodedContent content = new(values);
            HttpResponseMessage response = await requestClient.PostAsync("/users/edit", content);

            response.EnsureSuccessStatusCode();

            await usersClient.DeleteAsync(id);
        }

        [TestMethod]
        public async Task Delete_WithUserId_RemovesUsers()
        {
            UsersClient usersClient = new(apiClient);
            int id = int.MaxValue;
            await usersClient.PostAsync(new Web.Api.User
            {
                Id = id,
                FirstName = "John",
                LastName = "Smith"
            });

            var values = new Dictionary<string?, string?>
            {
                { nameof(UserViewModel.Id), id.ToString() }
            };
            using FormUrlEncodedContent content = new(values);
            HttpResponseMessage response = await requestClient.PostAsync("/users/delete", content);

            response.EnsureSuccessStatusCode();
            ICollection<Web.Api.User> users = await usersClient.GetAllAsync();
            Assert.IsFalse(users.Where(u => u.Id == id).Any());
        }
    }
}

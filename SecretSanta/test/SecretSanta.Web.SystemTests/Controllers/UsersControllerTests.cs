using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Web.Api;
using SecretSanta.Web.ViewModels;
using System.Linq;

namespace SecretSanta.Web.SystemTests.Controllers
{
    [TestClass]
    public class UsersControllerTests
    {
        HttpClient _ApiClient = new();
        HttpClient _RequestClient = new();
        [TestInitialize]
        public void Initialize()
        {
            if(_ApiClient.BaseAddress is null)
                _ApiClient.BaseAddress = new Uri("https://secretsantacasey-api.azurewebsites.net");
            if (_RequestClient.BaseAddress is null)
                _RequestClient.BaseAddress = new Uri("https://secretsantacasey.azurewebsites.net");
        }
        [TestMethod]
        public async Task Index_WithUsers_RetrievesUsers()
        {

            HttpResponseMessage response = await _RequestClient.GetAsync(new Uri("/users/index", UriKind.Relative));

            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public async Task Create_WithValidModel_CreatesUserAndReturnsToUsersIndex()
        {
            UsersClient usersClient = new(_ApiClient);
            int id = int.MaxValue;

            var values = new Dictionary<string, string>
            {
                { nameof(UserViewModel.Id), $"{id}" },
                { nameof(UserViewModel.FirstName), "John" },
                { nameof(UserViewModel.LastName), "Smith" },
            };
            using FormUrlEncodedContent content = new((IEnumerable<KeyValuePair<string?, string?>>)values);

            HttpResponseMessage response = await _RequestClient.PostAsync(new Uri("/users/create", UriKind.Relative), content);
            string responseContent = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Web.Api.UserDTO? createdUser = await usersClient.GetAsync(id);
            Assert.AreEqual(id, createdUser.Id);
            Assert.AreEqual("John", createdUser.FirstName);
            Assert.AreEqual("Smith", createdUser.LastName);
            Assert.IsTrue(responseContent.Contains("<title>Secret Santa - Users</title>"));

            //await usersClient.DeleteAsync(id);
        }

        [TestMethod]
        public async Task Create_WithInvalidModel_DoesNotCreatesUserAndReturnsUsersCreatePage()
        {
            UsersClient usersClient = new(_ApiClient);
            int id = int.MaxValue;
            string userFirstName = "UserFirstName";
            var values = new Dictionary<string, string>
            {
                { nameof(UserViewModel.Id), id.ToString() },
                { nameof(UserViewModel.FirstName), userFirstName },
                { nameof(UserViewModel.LastName), "" }
            };
            using FormUrlEncodedContent content = new((IEnumerable<KeyValuePair<string?, string?>>)values);

            HttpResponseMessage response = await _RequestClient.PostAsync(new Uri("/users/create", UriKind.Relative), content);
            string responseContent = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Assert.IsTrue(responseContent.Contains("<title>Secret Santa - Users - Create</title>"));
            Assert.IsTrue(responseContent.Contains($"id=\"FirstName\" name=\"FirstName\" value=\"{userFirstName}\""));
        }

        [TestMethod]
        public async Task Edit_WithUserId_RetrievesUser()
        {
            UsersClient usersClient = new(_ApiClient);
            int id = int.MaxValue;
            string userFirstName = "UserFirstName";
            await usersClient.PostAsync(new Web.Api.UserDTO
            {
                Id = id,
                FirstName = userFirstName,
                LastName = "Smith"
            });

            HttpResponseMessage response = await _RequestClient.GetAsync(new Uri($"/users/edit/{id}", UriKind.Relative));
            string responseContent = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Assert.IsTrue(responseContent.Contains("<title>Secret Santa - Users - Edit</title>"));
            Assert.IsTrue(responseContent.Contains($"id=\"FirstName\" name=\"FirstName\" value=\"{userFirstName}\""));

            await usersClient.DeleteAsync(id);
        }

        [TestMethod]
        public async Task Edit_WithValidModel_UpdatesUser()
        {
            UsersClient usersClient = new(_ApiClient);
            int id = int.MaxValue;
            string userFirstName = "UserFirstName";
            await usersClient.PostAsync(new Web.Api.UserDTO
            {
                Id = id,
                FirstName = "John",
                LastName = "Smith"
            });

            var values = new Dictionary<string, string>
            {
                { nameof(UserViewModel.Id), $"{id}" },
                { nameof(UserViewModel.FirstName), userFirstName },
                { nameof(UserViewModel.LastName), "Doe" },
            };
            using FormUrlEncodedContent content = new((IEnumerable<KeyValuePair<string?, string?>>)values);

            HttpResponseMessage response = await _RequestClient.PostAsync(new Uri("/users/edit", UriKind.Relative), content);
            string responseContent = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Web.Api.UserDTO? createdUser = await usersClient.GetAsync(id);
            Assert.AreEqual(id, createdUser.Id);
            Assert.AreEqual(userFirstName, createdUser.FirstName);
            Assert.AreEqual("Doe", createdUser.LastName);
            Assert.IsTrue(responseContent.Contains("<title>Secret Santa - Users</title>"));

            await usersClient.DeleteAsync(id);
        }

        [TestMethod]
        public async Task Edit_WithInvalidModel_DoesNotUpdateUser()
        {
            UsersClient usersClient = new(_ApiClient);
            int id = int.MaxValue;
            string userFirstName = "UserFirstName";
            await usersClient.PostAsync(new Web.Api.UserDTO
            {
                Id = id,
                FirstName = "John",
                LastName = "Smith"
            });

            var values = new Dictionary<string, string>
            {
                { nameof(UserViewModel.Id), id.ToString() },
                { nameof(UserViewModel.FirstName), userFirstName },
                { nameof(UserViewModel.LastName), "" },
            };
            using FormUrlEncodedContent content = new((IEnumerable<KeyValuePair<string?, string?>>)values);
            HttpResponseMessage response = await _RequestClient.PostAsync(new Uri("/users/edit", UriKind.Relative), content);

            response.EnsureSuccessStatusCode();

            await usersClient.DeleteAsync(id);
        }

        [TestMethod]
        public async Task Delete_WithUserId_RemovesUsers()
        {
            UsersClient usersClient = new(_ApiClient);
            int id = int.MaxValue;
            await usersClient.PostAsync(new Web.Api.UserDTO
            {
                Id = id,
                FirstName = "John",
                LastName = "Smith"
            });

            var values = new Dictionary<string, string>
            {
                { nameof(UserViewModel.Id), id.ToString() }
            };
            using FormUrlEncodedContent content = new((IEnumerable<KeyValuePair<string?, string?>>)values);
            HttpResponseMessage response = await _RequestClient.PostAsync(new Uri("/users/delete", UriKind.Relative), content);

            response.EnsureSuccessStatusCode();
            ICollection<Web.Api.UserDTO> users = await usersClient.GetAllAsync();
            Assert.IsFalse(users.Where(u => u.Id == id).Any());
        }
    }
}

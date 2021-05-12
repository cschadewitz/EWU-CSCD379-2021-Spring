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
using SecretSanta.Api.Controllers;
using SecretSanta.Api.DTO;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTests
    {
        WebApplicationFactory Factory;
        HttpClient Client;
        public Mock<IRepository<User>> MockUserRepository { get; } = new();
        public Mock<IMapper> MockMapper { get; } = new();

        private void Setup()
        {
            ConfigureMocks();
            Dictionary<Type, IEntityRepository> requiredEntityRepositories = new()
            {
                { typeof(IRepository<User>), MockUserRepository.Object }
            };
            Factory = new(requiredEntityRepositories);
            Client = Factory.CreateClient();

        }

        private void ConfigureMocks()
        {
            User mockUser = new User { Id = 1, FirstName = "Test", LastName = "User" };
            List<User> mockUsers = new List<User>
            {
                mockUser
            };
            MockUserRepository.Setup(r => r.List()).Returns((ICollection<User>)mockUsers);
            MockUserRepository.Setup(r => r.GetItem(1)).Returns(mockUser);
            MockUserRepository.Setup(r => r.GetItem(2)).Returns(null as User);
            MockUserRepository.Setup(r => r.Remove(1)).Returns(true);
            MockUserRepository.Setup(r => r.Remove(2)).Returns(false);
            MockUserRepository.Setup(r => r.Create(It.IsAny<User>())).Returns<User>(u => u);
            MockUserRepository.Setup(r => r.Create(null!)).Throws<ArgumentNullException>();
        }

        //Unit Tests

        [TestMethod]
        public void Constructor_GivenMissingUserRepositoryService_ThrowsArgumentNullException()
        {
            ArgumentNullException ex = Assert.ThrowsException<ArgumentNullException>(
                () => new UsersController(null!,  MockMapper.Object));
            Assert.AreEqual("userRepository", ex.ParamName);
        }
        [TestMethod]
        public void Constructor_GivenMissingMapperService_ThrowsArgumentNullException()
        {
            ArgumentNullException ex = Assert.ThrowsException<ArgumentNullException>(
                () => new UsersController(MockUserRepository.Object, null!));
            Assert.AreEqual("mapper", ex.ParamName);
        }

        //

        [TestMethod]
        public async Task Get_GivenData_ReturnsUserDTOs()
        {
            Setup();

            HttpResponseMessage response = await Client.GetAsync("/api/users/");
            IEnumerable<UserDTO>? userDTOs = await response.Content.ReadFromJsonAsync<IEnumerable<UserDTO>>();

            MockUserRepository.Verify(r => r.List(), Times.AtLeastOnce());
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(MockUserRepository.Object.List().Count, userDTOs.Count());
        }
        [TestMethod]
        public async Task Get_GivenValidId_ReturnsUser()
        {
            Setup();
            int id = 1;

            HttpResponseMessage response = await Client.GetAsync($"/api/users/{id}");
            UserDTO? userDTO = await response.Content.ReadFromJsonAsync<UserDTO>();

            MockUserRepository.Verify(r => r.GetItem(id), Times.AtLeastOnce());
            response.EnsureSuccessStatusCode();
            Assert.IsNotNull(userDTO);
            Assert.AreEqual(MockUserRepository.Object.GetItem(id).Id, userDTO.Id);
            Assert.AreEqual(MockUserRepository.Object.GetItem(id).FirstName, userDTO.FirstName);
            Assert.AreEqual(MockUserRepository.Object.GetItem(id).LastName, userDTO.LastName);
        }
        [TestMethod]
        public async Task Get_GivenInvalidId_ReturnsNotFound()
        {
            Setup();
            int id = 2;

            HttpResponseMessage response = await Client.GetAsync($"/api/users/{id}");
            UserDTO? userDTO = await response.Content.ReadFromJsonAsync<UserDTO>();

            MockUserRepository.Verify(r => r.GetItem(id), Times.AtLeastOnce());
            Assert.IsTrue(response.StatusCode is HttpStatusCode.NotFound);
        }
        [TestMethod]
        public async Task Delete_GivenValidId_ReturnsUser()
        {
            Setup();
            int id = 1;

            HttpResponseMessage response = await Client.DeleteAsync($"/api/users/{id}");

            MockUserRepository.Verify(r => r.Remove(id), Times.AtLeastOnce());
            response.EnsureSuccessStatusCode();
        }
        [TestMethod]
        public async Task Delete_GivenInvalidId_ReturnsNotFound()
        {
            Setup();
            int id = 2;

            HttpResponseMessage response = await Client.DeleteAsync($"/api/users/{id}");

            MockUserRepository.Verify(r => r.Remove(id), Times.AtLeastOnce());
            Assert.IsTrue(response.StatusCode is HttpStatusCode.NotFound);
        }
        [TestMethod]
        public async Task Post_GivenValidInput_ReturnsCreatedUser()
        {
            Setup();
            UserDTO userDTO = new UserDTO()
            {
                Id = 55,
                FirstName = "Another",
                LastName = "Test User"
            };

            HttpResponseMessage response = await Client.PostAsJsonAsync("/api/users/", userDTO);
            UserDTO? createdUserDTO = await response.Content.ReadFromJsonAsync<UserDTO>();

            MockUserRepository.Verify(r => r.Create(It.IsAny<User>()));
            response.EnsureSuccessStatusCode();
            Assert.IsNotNull(createdUserDTO);
            Assert.AreEqual(userDTO.Id, createdUserDTO.Id);
            Assert.AreEqual(userDTO.FirstName, createdUserDTO.FirstName);
            Assert.AreEqual(userDTO.LastName, createdUserDTO.LastName);
        }
        [TestMethod]
        public async Task Post_GivenNullInput_ReturnsBadRequest()
        {
            Setup();

            HttpResponseMessage response = await Client.PostAsJsonAsync("/api/users/", null as UserDTO);
            UserDTO? createdUserDTO = await response.Content.ReadFromJsonAsync<UserDTO>();

            MockUserRepository.Verify(r => r.Create(It.IsAny<User>()), Times.Never);
            Assert.IsTrue(response.StatusCode is HttpStatusCode.BadRequest);

        }
        [TestMethod]
        public async Task Put_GivenValidInputAndValidId_ReturnsSuccess()
        {
            Setup();
            int id = 1;
            UserDTO userDTO = new UserDTO()
            {
                Id = id,
                FirstName = "Another",
                LastName = "Test User"
            };

            HttpResponseMessage response = await Client.PutAsJsonAsync($"/api/users/{id}", userDTO);

            MockUserRepository.Verify(r => r.Save(It.IsAny<User>()));
            Assert.AreEqual(userDTO.FirstName, MockUserRepository.Object.GetItem(id)!.FirstName);
            Assert.AreEqual(userDTO.LastName, MockUserRepository.Object.GetItem(id)!.LastName);
        }
        [TestMethod]
        public async Task Put_GivenNullInputAndValidId_ReturnsBadRequest()
        {
            Setup();
            int id = 1;

            HttpResponseMessage response = await Client.PutAsJsonAsync($"/api/users/{id}", null as UserDTO);

            MockUserRepository.Verify(r => r.Save(It.IsAny<User>()), Times.Never);
            Assert.IsTrue(response.StatusCode is HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public async Task Put_GivenValidInputAndInvalidId_ReturnsSuccess()
        {
            Setup();
            int id = 2;
            UserDTO userDTO = new UserDTO()
            {
                Id = id,
                FirstName = "Another",
                LastName = "Test User"
            };

            HttpResponseMessage response = await Client.PutAsJsonAsync($"/api/users/{id}", userDTO);

            MockUserRepository.Verify(r => r.Save(It.IsAny<User>()), Times.Never);
            Assert.IsTrue(response.StatusCode is HttpStatusCode.NotFound);
        }
    }
}

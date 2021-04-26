using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecertSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTests
    {

        Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        Mock<User> mockUser = new Mock<User>();

        [TestMethod]
        public void Constructor_WithNullUserRepository_ThrowsArgumentNullException()
        {
            ArgumentNullException ex = Assert.ThrowsException<ArgumentNullException>(
                () => new UsersController(null!));
            Assert.AreEqual("userRepository", ex.ParamName);
        }

        [TestMethod]
        public void Get_WithData_ReturnsUsers()
        {
            //Arrange
            MockSetup();
            UsersController controller = new(mockUserRepository.Object);

            //Act
            IEnumerable<User> users = controller.Get();

            //Assert
            Assert.IsTrue(users.Any());
        }

        [TestMethod]
        public void Get_WithNoData_ReturnsEmpty()
        {
            //Arrange
            MockSetupEmpty();
            UsersController controller = new(mockUserRepository.Object);

            //Act
            IEnumerable<User> users = controller.Get();

            //Assert
            Assert.IsFalse(users.Any());
        }

        [TestMethod]
        [DataRow(3)]
        public void Get_WithExistingId_ReturnsUserFromList(int id)
        {
            //Arrange
            MockSetup();
            UsersController controller = new(mockUserRepository.Object);

            //Act
            ActionResult<User?> result = controller.Get(id);

            //Assert
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(result.Value.Id, id);
            Assert.AreEqual(result.Value.FirstName, mockUserRepository.Object.List().First(u => u.Id == id).FirstName);
            Assert.AreEqual(result.Value.LastName, mockUserRepository.Object.List().First(u => u.Id == id).LastName);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(5)]
        public void Get_WithNonExistantId_ReturnsNotFound(int id)
        {
            //Arrange
            MockSetup();
            UsersController controller = new(mockUserRepository.Object);

            //Act
            ActionResult<User?> result = controller.Get(id);

            //Assert
            Assert.IsTrue(result.Result is NotFoundResult);
        }

        [TestMethod]
        public void Create_WithNonNullUser_ReturnsUser()
        {
            //Arrange
            MockSetup();
            UsersController controller = new(mockUserRepository.Object);
            //Act
            ActionResult<User?> result = controller.Create(mockUser.Object);
            //Assert
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(result.Value.Id, mockUser.Object.Id);
            Assert.AreEqual(result.Value.FirstName, mockUser.Object.FirstName);
            Assert.AreEqual(result.Value.LastName, mockUser.Object.LastName);
        }

        [TestMethod]
        public void Create_WithNullUser_ReturnsBadRequest()
        {
            //Arrange
            UsersController controller = new(mockUserRepository.Object);
            //Act
            ActionResult<User?> result = controller.Create(null);
            //Assert
            Assert.IsTrue(result.Result is BadRequestObjectResult);
        }

        [TestMethod]
        public void Edit_WithExistingIdAndNullUser_ReturnBadRequest()
        {

            //Arrange
            MockSetup();
            UsersController controller = new(mockUserRepository.Object);
            //Act
            ActionResult<User?> result = controller.Edit(0, null);
            //Assert
            Assert.IsTrue(result.Result is BadRequestResult);
        }

        [TestMethod]
        public void Edit_WithNonExistantIdAndUser_ReturnsNotFound()
        {

            //Arrange
            MockSetup();
            UsersController controller = new(mockUserRepository.Object);
            //Act
            ActionResult<User?> result = controller.Edit(-1, mockUser.Object);
            //Assert
            Assert.IsTrue(result.Result is NotFoundResult);
        }

        [TestMethod]
        public void Edit_WithExistingIdAndUser_ReturnsOk()
        {
            //Arrange
            MockSetup();
            UsersController controller = new(mockUserRepository.Object);
            //Act
            ActionResult<User?> result = controller.Edit(0, mockUser.Object);
            //Assert
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(result.Value.FirstName, mockUser.Object.FirstName);
            Assert.AreEqual(result.Value.LastName, mockUser.Object.LastName);
        }

        [TestMethod]
        public void Delete_WithNonExistantId_ReturnsNotFound()
        {
            //Arrange
            MockSetup();
            UsersController controller = new(mockUserRepository.Object);
            //Act
            ActionResult result = controller.Delete(-1);
            //Assert
            Assert.IsTrue(result is NotFoundResult);
        }

        [TestMethod]
        public void Delete_WithExistingId_ReturnsOk()
        {
            //Arrange
            MockSetup();
            UsersController controller = new(mockUserRepository.Object);
            //Act
            ActionResult result = controller.Delete(0);
            //Assert
            Assert.IsTrue(result is OkResult);
        }


        //Mock Stuff
        private void MockSetup()
        {
            DataCollection<User> mockData = new DataCollection<User>
                {
                    new User { Id = 0, FirstName = "Inigo", LastName = "Montoya" },
                    new User { Id = 1, FirstName = "Princess", LastName = "Buttercup" },
                    new User { Id = 2, FirstName = "Prince", LastName = "Humperdink" },
                    new User { Id = 3, FirstName = "Count", LastName = "Rugen" },
                    new User { Id = 4, FirstName = "Miracle", LastName = "Max" },
                };
            mockUserRepository.Setup(r => r.List()).Returns(mockData);
            mockUserRepository.Setup(r => r.GetItem(It.IsAny<int>())).Returns<int>(i =>
            {
                return mockData[i];
            });
            mockUserRepository.Setup(r => r.Create(It.IsAny<User>())).Returns<User>(u =>
            {
                mockData.Add(u);
                return u;
            });
            mockUserRepository.Setup(r => r.Remove(It.IsAny<int>())).Returns<int>(i =>
            {
                if (mockData[i] is null)
                    return false;
                return true;
            });
            mockUserRepository.Setup(r => r.Save(It.IsAny<User>())).Callback<User>(u => mockData[u.Id] = u);

            mockUser.SetupGet(p => p.Id).Returns(5);
            mockUser.SetupGet(p => p.FirstName).Returns("That One");
            mockUser.SetupGet(p => p.LastName).Returns("Guy");
        }
        private void MockSetupEmpty()
        {
            mockUserRepository.Setup(r => r.List()).Returns(new DataCollection<User>());
        }
    }
}

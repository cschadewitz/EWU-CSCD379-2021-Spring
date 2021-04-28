using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {
        Mock<User> mockValidUser = new Mock<User>();
        Mock<User> mockEditedValidUser = new Mock<User>();
        Mock<User> mockEditedInvalidUser = new Mock<User>();

        [TestMethod]
        public void Create_WithValidUser_AppearsInList()
        {
            //Arrange
            MockSetup();
            UserRepository repo = new();
            int count = StaticData.Users.Count;

            //Act
            User createdUser = repo.Create(mockValidUser.Object);

            //Assert
            Assert.AreEqual(count + 1, StaticData.Users.Count);
            Assert.AreEqual(mockValidUser.Object.FirstName, createdUser.FirstName);
            Assert.AreEqual(mockValidUser.Object.LastName, createdUser.LastName);

        }

        [TestMethod]
        public void GetItem_WithExistingID_ReturnsUser()
        {
            //Arrange
            MockSetup();
            UserRepository repo = new();
            int inputId = 1;

            //Act
            User? retrievedUser = repo.GetItem(inputId);

            //Assert
            Assert.AreEqual(retrievedUser.Id, inputId);
        }

        [TestMethod]
        public void GetItem_WithNonExistantID_ReturnsNull()
        {
            //Arrange
            MockSetup();
            UserRepository repo = new();
            int inputId = -1;

            //Act
            User? retrievedUser = repo.GetItem(inputId);

            //Assert
            Assert.AreEqual(retrievedUser, null);
        }

        [TestMethod]
        public void List_WithData_ReturnsList()
        {
            //Arrange
            MockSetup();
            UserRepository repo = new();

            //Act
            ICollection<User> retrievedUsers = repo.List();

            //Assert
            Assert.IsTrue(retrievedUsers.Any());
        }

        [TestMethod]
        public void Remove_WithExistingID_ReturnsTrue()
        {
            //Arrange
            MockSetup();
            UserRepository repo = new();
            int inputId = 1;
            int count = StaticData.Users.Count;

            //Act
            bool flag = repo.Remove(inputId);

            //Assert
            Assert.IsTrue(flag);
            Assert.AreEqual(count - 1, StaticData.Users.Count);
            Assert.AreEqual(StaticData.Users[inputId], null);
        }

        [TestMethod]
        public void Save_WithExistingIDAndNonNullUseer_UserSaved()
        {
            //Arrange
            MockSetup();
            UserRepository repo = new();

            //Act
            repo.Save(mockEditedValidUser.Object);

            //Assert
            User ActualUser = StaticData.Users[mockEditedValidUser.Object.Id];
            Assert.AreEqual(mockEditedValidUser.Object.FirstName + mockEditedValidUser.Object.LastName, ActualUser.FirstName + ActualUser.LastName);
        }

        //Mock Stuff
        private void MockSetup()
        {
            mockValidUser.SetupGet(p => p.Id).Returns(5);
            mockValidUser.SetupGet(p => p.FirstName).Returns("That One");
            mockValidUser.SetupGet(p => p.LastName).Returns("Guy");

            mockEditedValidUser.SetupGet(p => p.Id).Returns(0);
            mockEditedValidUser.SetupGet(p => p.FirstName).Returns("That Other");
            mockEditedValidUser.SetupGet(p => p.LastName).Returns("Guy");

            mockEditedInvalidUser.SetupGet(p => p.Id).Returns(-1);
            mockEditedInvalidUser.SetupGet(p => p.FirstName).Returns("That Third");
            mockEditedInvalidUser.SetupGet(p => p.LastName).Returns("Guy");
        }

    }
}

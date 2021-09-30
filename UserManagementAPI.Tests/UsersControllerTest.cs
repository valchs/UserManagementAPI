using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using UserManagementAPI.Controllers;
using UserManagementLibrary.DataAccess;
using UserManagementLibrary.Models;
using Xunit;

namespace UserManagementAPI.Tests
{
    public class UsersControllerTest
    {
        [Fact]
        public void Get_Returns_Correct_Number_Of_Users()
        {
            // Arrange
            var count = 3;
            var dataStore = A.Fake<IUserData>();
            var fakeUsers = (List<User>)A.CollectionOfDummy<User>(count);
            A.CallTo(() => dataStore.GetUsers()).Returns(fakeUsers);
            var controller = new UsersController(dataStore);

            //Act
            var actionResult = controller.Get();

            //Assert
            var result = actionResult.Result as OkObjectResult;
            var returnUsers = result.Value as List<User>;
            Assert.Equal(count, returnUsers.Count);
        }
    }
}

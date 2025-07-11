using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Users.Api.Controllers;
using Users.Api.Models;
using Xunit;

namespace User.Api.Tests
{
    public class UsersTests
    {
        [Fact]
        public void CreateUser_InvalidEmail_ReturnsBadRequest()
        {
            // Arrange
            var controller = new UsersController();
            var user = new UserModel
            {
                FullName = "Jane Doe",
                Email = "invalid-email",
                DateOfBirth = new DateTime(1990, 1, 1)
            };

            // Act
            var result = controller.CreateUser(user);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Valid Email is required", badRequest.Value.ToString());
        }

        [Fact]
        public void DeleteUser_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var controller = new UsersController();

            // Act
            var result = controller.DeleteUser(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Users.Api.Controllers;
using Users.Api.Models;
using Users.Api.Validators;
using Xunit;

namespace User.Api.Tests;

public class UsersTests
{
    [Fact]
    public void FutureDateOfBirth_ShouldHaveValidationError()
    {
        var validator = new UserModelValidator();
        var model = new UserModel
        {
            FullName = "Future Person",
            Email = "future@example.com",
            DateOfBirth = DateTime.UtcNow.AddDays(1)
        };

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(u => u.DateOfBirth)
              .WithErrorMessage("DateOfBirth cannot be in the future.");
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

    [Fact]
    public void MissingEmail_ShouldHaveValidationError()
    {
        // Arrange
        var validator = new UserModelValidator();
        var user = new UserModel
        {
            FullName = "Jane Doe",
            Email = "",
            DateOfBirth = new DateTime(1990, 1, 1)
        };

        // Act
        var result = validator.TestValidate(user);

        // Assert
        result.ShouldHaveValidationErrorFor(u => u.Email)
              .WithErrorMessage("Email is required.");
    }

    [Fact]
    public void FullNameTooShort_ShouldHaveValidationError()
    {
        // Arrange
        var validator = new UserModelValidator();
        var user = new UserModel
        {
            FullName = "A",
            Email = "a@example.com",
            DateOfBirth = new DateTime(1990, 1, 1)
        };

        // Act
        var result = validator.TestValidate(user);

        // Assert
        result.ShouldHaveValidationErrorFor(u => u.FullName)
              .WithErrorMessage("FullName must be at least 2 characters long.");
    }

    [Fact]
    public void MissingFullName_ShouldHaveValidationError()
    {
        // Arrange
        var validator = new UserModelValidator();
        var user = new UserModel
        {
            FullName = "",
            Email = "a@example.com",
            DateOfBirth = new DateTime(1990, 1, 1)
        };

        // Act
        var result = validator.TestValidate(user);

        // Assert
        result.ShouldHaveValidationErrorFor(u => u.FullName)
              .WithErrorMessage("FullName is required.");
    }
}
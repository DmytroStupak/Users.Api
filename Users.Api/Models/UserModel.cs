﻿namespace Users.Api.Models;

public class UserModel
{
    public int Id { get; set; }

    public required string FullName { get; set; }

    public required string Email { get; set; }

    public DateTime DateOfBirth { get; set; }
}

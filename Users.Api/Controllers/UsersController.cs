using Microsoft.AspNetCore.Mvc;
using Users.Api.Models;

namespace Users.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private static readonly List<UserModel> Users = new();
    private static int NextId = 1;

    [HttpGet]
    [ProducesResponseType(typeof(List<UserModel>), StatusCodes.Status200OK)]
    public IActionResult GetUsers() => Ok(Users);

    [HttpPost]
    [ProducesResponseType(typeof(UserModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateUser([FromBody] UserModel user)
    {
        // Validation
        if (string.IsNullOrWhiteSpace(user.FullName) || user.FullName.Length < 2)
            return BadRequest("FullName is required and must be at least 2 characters.");
        if (string.IsNullOrWhiteSpace(user.Email) || !IsValidEmail(user.Email))
            return BadRequest("Valid Email is required.");
        if (user.DateOfBirth > DateTime.UtcNow)
            return BadRequest("DateOfBirth cannot be in the future.");

        user.Id = NextId++;
        Users.Add(user);

        return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteUser(int id)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();
        Users.Remove(user);
        return NoContent();
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}

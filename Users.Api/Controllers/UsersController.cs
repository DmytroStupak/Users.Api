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
}

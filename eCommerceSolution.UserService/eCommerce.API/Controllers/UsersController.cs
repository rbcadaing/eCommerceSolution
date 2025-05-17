using eCommerce.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserByUserID(Guid userId)
    {
        if (userId == Guid.Empty) { return BadRequest("Invalid User ID"); }
        var user = await _userService.GetUserByUserID(userId);
        if (user == null)
        {
            return NotFound("User not found");
        }
        return Ok(user);
    }
}

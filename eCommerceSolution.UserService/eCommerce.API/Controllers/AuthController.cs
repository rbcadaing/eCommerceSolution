using eCommerce.Core.Dto;
using eCommerce.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            if (registerRequest == null) { return BadRequest("Invalid Registration"); }

            var authenticationResponse = await _userService.Register(registerRequest);
            if (authenticationResponse == null ||
                authenticationResponse.Success == false)
            {

                return BadRequest(authenticationResponse);

            }

            return Ok(authenticationResponse);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Invalid Login Data");
            }
            var authenticationResponse = await _userService.Login(loginRequest);

            if (authenticationResponse == null ||
               authenticationResponse.Success == false)
            {

                return Unauthorized(authenticationResponse);

            }
            return Ok(authenticationResponse);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions.Services;
using Shared.Dtos.Auth;
using System.Security.Claims;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LogInRequest logIn)
        {

            var result = await _serviceManager.AuthService.LogInAsync(logIn);

            return Ok(result);

        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {

            var result = await _serviceManager.AuthService.RegisterAsync(request);

            return Ok(result);

        }

        [HttpDelete("Delete")]

        public async Task<IActionResult> DeleteAcount(string email)
        {
            await _serviceManager.AuthService.DeleteAsync(email);

            return NoContent();
        }

        //Check Email Existence
        [HttpGet("EmailExists")]
        public async Task<IActionResult> ChechEmailExsists(string email)
        {

            var result = await _serviceManager.AuthService.CheckEmailExistAsync(email);

            return Ok(result);
        }

        //Get Current User
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.AuthService.GetCurrentUserAsync(email.Value);

            return Ok(result);
        }

        //Get Current User Address

        [Authorize]
        [HttpGet("Address")]
        public async Task<IActionResult> GetCurrentUserAddress()
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.AuthService.GetCurrentUserAddressAsync(email.Value);

            return Ok(result);
        }

        // Update Current User Address

        [Authorize]
        [HttpPut("Address")]
        public async Task<IActionResult> UpdateCurrentUserAddress(AddressDto request)
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.AuthService.UpdateCurrentUserAddressAsync(request, email.Value);

            return Ok(result);
        }

    }
}

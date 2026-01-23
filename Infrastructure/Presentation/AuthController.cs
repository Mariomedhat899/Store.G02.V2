using Microsoft.AspNetCore.Mvc;
using Services.Abstractions.Services;
using Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}

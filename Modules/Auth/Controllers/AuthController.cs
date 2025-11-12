using cubets_core.Common;
using cubets_core.Modules.Auth.DTOs;
using cubets_core.Modules.Auth.Models;
using cubets_core.Modules.Auth.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cubets_core.Modules.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IResponseService _response;
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService, IResponseService response)
        {
            _response = response;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            return Ok(_response.Success(result, "Register Successful!"));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(_response.Success(result, "Login Successful!"));
        }

        [HttpPost("guest")]
        public async Task<IActionResult> Guest()
        {
            var result = await _authService.GuestLoginAsync();
            return Ok(_response.Success(result, "Login as Guest Successful!"));
        }
    }
}

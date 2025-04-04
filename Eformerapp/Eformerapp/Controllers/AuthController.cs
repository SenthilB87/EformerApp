// Controllers/AuthController.cs
using Eformerapp.DTOs;
using Eformerapp.Data.Entities;
using Eformerapp.Data.Repositories;
using Eformerapp.Data.Repository.Interface;
using Eformerapp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Eformerapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthService _authService;

        public AuthController(IUserRepository userRepository, AuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto loginDto)
        {
            // Get user with role included
            var user = await _userRepository.GetByMobileNumberAsync(loginDto.MobileNumber);

            if (user == null || user.IsDeleted)
            {
                return Unauthorized("Invalid mobile number");
            }

            // Generate JWT token with all required claims
            var token = _authService.GenerateJwtToken(user);

            return Ok(new LoginResponseDto { Token = token });
        }
    }
}
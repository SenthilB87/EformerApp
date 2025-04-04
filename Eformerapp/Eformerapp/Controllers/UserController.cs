
using Eformerapp.DTOs;
using Eformerapp.Data.Entities;
using Eformerapp.Data.Repositories;
using Eformerapp.Data.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eformerapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/Users
        [HttpPost("getall")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                userDtos.Add(MapToDto(user));
            }

            return Ok(userDtos);
        }

        // POST: api/Users/getbyid
        [HttpPost("getbyid")]
        public async Task<ActionResult<UserDto>> GetUserById([FromBody] int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(MapToDto(user));
        }

        // POST: api/Users/getbyname
        [HttpPost("getbyname")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersByName([FromBody] string name)
        {
            var users = await _userRepository.GetUsersByNameAsync(name);
            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                userDtos.Add(MapToDto(user));
            }

            return Ok(userDtos);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var user = new User
            {
                Name = createUserDto.Name,
                MobileNumber = createUserDto.MobileNumber,
                Address1 = createUserDto.Address1,
                Address2 = createUserDto.Address2,
                Address3 = createUserDto.Address3,
                City = createUserDto.City,
                Pincode = createUserDto.Pincode,
                UserRoleId = createUserDto.UserRoleId,
                IsDeleted = false
            };

            var createdUser = await _userRepository.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, MapToDto(createdUser));
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Name = updateUserDto.Name;
            existingUser.MobileNumber = updateUserDto.MobileNumber;
            existingUser.Address1 = updateUserDto.Address1;
            existingUser.Address2 = updateUserDto.Address2;
            existingUser.Address3 = updateUserDto.Address3;
            existingUser.City = updateUserDto.City;
            existingUser.Pincode = updateUserDto.Pincode;
            existingUser.UserRoleId = updateUserDto.UserRoleId;

            await _userRepository.UpdateUserAsync(existingUser);

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userRepository.DeleteUserAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        private UserDto MapToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                MobileNumber = user.MobileNumber,
                Address1 = user.Address1,
                Address2 = user.Address2,
                Address3 = user.Address3,
                City = user.City,
                Pincode = user.Pincode,
                UserRoleId = user.UserRoleId,
                UserRoleName = user.UserRole?.Name // Include UserRoleName
            };
        }
    }
}
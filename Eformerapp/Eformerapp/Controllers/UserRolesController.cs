
using Eformerapp.DTOs;
using Eformerapp.Data.Entities;
using Eformerapp.Data.Repositories;
using Eformerapp.Data.Repository.Interface;
using Eformerapp.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eformerapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRolesController(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        // POST: api/UserRoles/getall
        [HttpPost("getall")]
        public async Task<ActionResult<IEnumerable<UserRoleDto>>> GetAll()
        {
            var userRoles = await _userRoleRepository.GetAllAsync();
            var userRoleDtos = new List<UserRoleDto>();

            foreach (var userRole in userRoles)
            {
                userRoleDtos.Add(MapToDto(userRole));
            }

            return Ok(userRoleDtos);
        }

        // POST: api/UserRoles/getbyid
        [HttpPost("getbyid")]
        public async Task<ActionResult<UserRoleDto>> GetById([FromBody] int id)
        {
            var userRole = await _userRoleRepository.GetByIdAsync(id);

            if (userRole == null)
            {
                return NotFound();
            }

            return Ok(MapToDto(userRole));
        }

        // POST: api/UserRoles/getbyname
        [HttpPost("getbyname")]
        public async Task<ActionResult<IEnumerable<UserRoleDto>>> GetByName([FromBody] string name)
        {
            var userRoles = await _userRoleRepository.GetByNameAsync(name);
            var userRoleDtos = new List<UserRoleDto>();

            foreach (var userRole in userRoles)
            {
                userRoleDtos.Add(MapToDto(userRole));
            }

            return Ok(userRoleDtos);
        }

        // POST: api/UserRoles
        [HttpPost]
        public async Task<ActionResult<UserRoleDto>> Create([FromBody] CreateUserRoleDto createUserRoleDto)
        {
            var userRole = new UserRole
            {
                Name = createUserRoleDto.Name
            };

            var createdUserRole = await _userRoleRepository.CreateAsync(userRole);
            return CreatedAtAction(nameof(GetById), new { id = createdUserRole.Id }, MapToDto(createdUserRole));
        }

        // POST: api/UserRoles/update
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserRoleDto updateUserRoleDto)
        {
            var existingUserRole = await _userRoleRepository.GetByIdAsync(updateUserRoleDto.Id);
            if (existingUserRole == null)
            {
                return NotFound();
            }

            existingUserRole.Name = updateUserRoleDto.Name;

            await _userRoleRepository.UpdateAsync(existingUserRole);
            return NoContent();
        }

        private UserRoleDto MapToDto(UserRole userRole)
        {
            return new UserRoleDto
            {
                Id = userRole.Id,
                Name = userRole.Name
            };
        }
    }
}
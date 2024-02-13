using Api.DTOs;
using Api.Mappers;
using Api.Models;
using Api.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private UserMapper _userMapper;

        public UserController(IUserService userService, UserMapper userMapper)
        {
            _userService = userService;
            _userMapper = userMapper;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<User>>> Get()
        {
            var users = await _userService.GetUsersAsync();
            if (users is null)
            {
                return NoContent();
            }
            return Ok(_userMapper.MapUserListToDtoList(users));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user is null)
            {
                return NotFound();
            }
            return Ok(_userMapper.MapUserToDto(user));
        }
        [HttpPut("{id}/update")]
        public async Task<ActionResult> Update(int id, UserCredentialsDto creds)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user is null)
            {
                return NotFound();
            }
            await _userService.UpdateUserCredentials(user, creds);
            return Ok();
        }
    }
}

using Api.DTOs;
using Api.Models;
using Api.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto request)
        {
            if (await IsTaken(request.Username))
            {
                return Conflict("Such username already exists");
            }
            if (!IsValid(request))
            {
                return BadRequest("Cannot register");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            await _userService.AddUserAsync(newUser);
            await _userService.AssignUserToRoleAsync(newUser);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto request)
        {
            var user = await _userService.GetUserByUsernameAsync(request.Username);
            if (user is null || !VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("User not found or wrong password.");
            }

            var roles = await _userService.GetUserRolesAsync(user.Id);

            string token = CreateToken(user, roles);
            return Ok(token);
        }

        private string CreateToken(User user, IEnumerable<string> roles)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHach = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHach.SequenceEqual(passwordHash);
            }
        }

        private async Task<bool> IsTaken(string username)
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            return user is not null;
        }

        private bool IsValid(RegisterDto request)
        {
            if (request.Username.Length >= 4 && request.Password.Length >= 6)
            {
                return true;
            }
            return false;
        }
    }
}

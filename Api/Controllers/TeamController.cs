using Api.DTOs;
using Api.Models;
using Api.Services.TeamService;
using Api.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IUserService _userService;

        public TeamController(ITeamService teamService, IUserService userService)
        {
            _teamService = teamService;
            _userService = userService;
        }
        [HttpGet("all")]
        public async Task<ActionResult<List<Team>>> Get()
        {
            return await _teamService.GetTeamsAsync();
        }
        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult> Create(TeamRequestDto request)
        {
            if (await IsTaken(request.Name))
            {
                return Conflict(new { message = "Team with such name already exist", reason = "NameTaken"});
            }
            if (!IsValid(request.Name))
            {
                return BadRequest("Cannot register");
            }

            var user = await _userService.GetUserByIdAsync(request.UserId);
            if (user is null)
            {
                return BadRequest("Something went wrong");
            }
            if (user.Team is not null)
            {
                return Conflict(new { message = "User is already a member of a team", reason = "UserAlreadyInTeam" });
            }
            var team = new Team { Name = request.Name };

            await _teamService.CreateTeamAsync(team, user);
            await _userService.AssignUserToLeaderRoleAsync(user);

            return Ok();
        }

        private async Task<bool> IsTaken(string name)
        {
            var team = await _teamService.GetTeamByNameAsync(name);
            return team is not null;
        }
        private bool IsValid(string name)
        {
            if (name.Length >= 2 && name.Length <= 50)
            {
                return true;
            }
            return false;
        }
    }
}

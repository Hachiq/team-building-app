using Api.Mappers;
using Api.Models;
using Api.RequestDTOs;
using Api.ResponseDTOs;
using Api.Services.RequestService;
using Api.Services.StatsService;
using Api.Services.TeamService;
using Api.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IUserService _userService;
        private readonly IRequestService _requestService;
        private readonly IStatsService _statsService;
        private readonly TeamMapper _teamMapper;
        private readonly UserMapper _userMapper;
        private readonly RequestMapper _requestMapper;

        public TeamController(ITeamService teamService, IUserService userService, IRequestService requestService, IStatsService statsService, TeamMapper mapper, UserMapper userMapper, RequestMapper requestMapper)
        {
            _teamService = teamService;
            _userService = userService;
            _requestService = requestService;
            _statsService = statsService;
            _teamMapper = mapper;
            _userMapper = userMapper;
            _requestMapper = requestMapper;
        }
        [HttpGet("all")]
        public async Task<ActionResult<List<TeamDto>>> Get()
        {
            var teams = await _teamService.GetTeamsAsync();
            if (teams.IsNullOrEmpty())
            {
                return NoContent();
            }
            return Ok(_teamMapper.MapTeamListToDtoList(teams));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDto>> GetById(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return Ok(_teamMapper.MapTeamToDto(team));
        }
        [HttpGet("{id}/users")]
        public async Task<ActionResult<List<User>>> GetUsersByTeamId(int id)
        {
            var users = await _userService.GetUsersByTeamIdAsync(id);
            if (users is null)
            {
                return NoContent();
            }
            return Ok(_userMapper.MapUserListToDtoList(users));
        }
        [HttpGet("{id}/requests")]
        public async Task<ActionResult<List<Request>>> GetRequestsByTeamId(int id)
        {
            var requests = await _requestService.GetRequestsByTeamIdAsync(id);
            if (requests is null)
            {
                return NoContent();
            }
            return Ok(_requestMapper.MapRequestListToDtoList(requests));
        }
        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult> Create(CreateTeamDto request)
        {
            if (await IsTaken(request.Name))
            {
                return Conflict(new { message = "Team with such name already exist", reason = "NameTaken" });
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

        [Authorize(Roles = "Leader")]
        [HttpPost("{id}/disband")]
        public async Task<ActionResult> Disband(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team is null)
            {
                return NotFound();
            }
            var leader = await _teamService.GetTeamLeaderAsync(team);
            if (leader is null)
            {
                return NotFound();
            }
            await _requestService.DeclineRequestsOnDisbandAsync(team);
            await _statsService.ResetStatsOnDisbandAsync(team);
            await _teamService.DisbandTeamAsync(team);
            await _userService.RemoveUserFromLeaderRoleAsync(leader);
            return Ok();
        }

        [HttpPost("{id}/leave")]
        public async Task<ActionResult> Leave(int id, [FromBody] int userId)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team is null)
            {
                return NotFound();
            }
            var user = await _userService.GetUserByIdAsync(userId);
            if (user is null || user.Stats is null)
            {
                return NotFound();
            }
            await _teamService.RemoveUserFromTeamAsync(team, user);
            await _statsService.ResetStatsOnLeaveAsync(user.Stats);
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

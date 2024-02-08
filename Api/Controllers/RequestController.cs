using Api.DTOs;
using Api.Mappers;
using Api.Models;
using Api.Services.RequestService;
using Api.Services.TeamService;
using Api.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly ITeamService _teamService;
        private readonly IUserService _userService;
        private readonly RequestMapper _requestMapper;

        public RequestController(IRequestService requestService, RequestMapper requestMapper, ITeamService teamService, IUserService userService)
        {
            _requestService = requestService;
            _requestMapper = requestMapper;
            _teamService = teamService;
            _userService = userService;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(CreateJoinRequestDto joinRequest)
        {
            if (joinRequest.UserId == 0 || joinRequest.TeamId == 0)
            {
                return BadRequest("Something went wrong.");
            }
            var user = await _userService.GetUserByIdAsync(joinRequest.UserId);
            if (user.Team is not null)
            {
                return Conflict("User is already a member of a team.");
            }
            await _requestService.CreateRequestAsync(joinRequest.UserId, joinRequest.TeamId);
            return Ok();
        }

        [Authorize(Roles = "Leader")]
        [HttpPut("{id}/accept")]
        public async Task<ActionResult> Accept(int id)
        {
            var request = await _requestService.GetRequestByIdAsync(id);
            if (request is null)
            {
                return NotFound();
            }
            if (_requestService.AlreadyProcessed(request))
            {
                return BadRequest("Request was already processed.");
            }
            
            var user = await _userService.GetUserByIdAsync(request.UserId);
            var team = await _teamService.GetTeamByIdAsync(request.TeamId);

            await _teamService.AddTeamMemberAsync(team, user);
            await _requestService.AcceptRequestAsync(request);

            return Ok();
        }

        [Authorize(Roles = "Leader")]
        [HttpPut("{id}/decline")]
        public async Task<ActionResult> Decline(int id)
        {
            var request = await _requestService.GetRequestByIdAsync(id);
            if (request is null)
            {
                return NotFound();
            }
            if (_requestService.AlreadyProcessed(request))
            {
                return BadRequest("Request was already processed.");
            }
            await _requestService.DeclineRequestAsync(request);

            return Ok();
        }
    }
}

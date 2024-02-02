using Api.DTOs;
using Api.Mappers;
using Api.Models;
using Api.Services.RequestService;
using Api.Services.TeamService;
using Api.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
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
        [HttpGet("{teamId}")]
        public async Task<ActionResult<List<Request>>> Get(int teamId)
        {
            if (teamId is 0)
            {
                return BadRequest("Something went wrong.");
            }
            var requests = await _requestService.GetRequestsByTeamIdAsync(teamId);
            return Ok(_requestMapper.MapRequestListToDtoList(requests));
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(CreateRequestDto joinRequest)
        {
            if (joinRequest.UserId is 0 || joinRequest.TeamId is 0)
            {
                return BadRequest("Something went wrong.");
            }
            await _requestService.CreateRequestAsync(joinRequest.UserId, joinRequest.TeamId);
            return Ok();
        }

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

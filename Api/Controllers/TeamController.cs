using Api.Models;
using Api.Services.TeamService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        [HttpGet("all")]
        public async Task<ActionResult<List<Team>>> Get()
        {
            return await _teamService.GetTeamsAsync();
        }
    }
}

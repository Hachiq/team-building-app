using Api.Mappers;
using Api.Models;
using Api.Services.StatsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IStatsService _statsService;
        private readonly StatsMapper _statsMapper;

        public StatsController(IStatsService statsService, StatsMapper statsMapper)
        {
            _statsService = statsService;
            _statsMapper = statsMapper;
        }

        [HttpGet("user")]
        public async Task<ActionResult<Stats>> Get(int id)
        {
            var stats = await _statsService.GetStatsByUserIdAsync(id);
            if (stats == null)
            {
                return NotFound();
            }
            return Ok(_statsMapper.MapStatsToDto(stats));
        }
        [HttpGet("team")]
        public async Task<ActionResult<Stats>> GetTeamStats(int id)
        {
            var stats = await _statsService.GetStatsByTeamIdAsync(id);
            if (stats.Count == 0)
            {
                return NotFound();
            }
            return Ok(_statsMapper.MapStatsToTeamStatsDto(stats));
        }
        [Authorize(Roles = "Leader")]
        [HttpPut("{id}/salary")]
        public async Task<ActionResult> UpdateSalary(int id, [FromBody] int salary)
        {
            var stats = await _statsService.GetStatsByUserIdAsync(id);
            await _statsService.UpdateUserSalaryAsync(stats, salary);
            return Ok();
        }
        [Authorize(Roles = "Leader")]
        [HttpPut("{id}/add-day-worked")]
        public async Task<ActionResult> AddDayWorked(int id) 
        {
            var stats = await _statsService.GetStatsByUserIdAsync(id);
            if (stats is null)
            {
                return NotFound();
            }
            await _statsService.AddDayWorkedAsync(stats);
            return Ok();
        }
        [Authorize(Roles = "Leader")]
        [HttpPut("{id}/add-day-paid")]
        public async Task<ActionResult> AddDayPaid(int id)
        {
            var stats = await _statsService.GetStatsByUserIdAsync(id);
            if (stats is null)
            {
                return NotFound();
            }
            await _statsService.AddDayPaidAsync(stats);
            return Ok();
        }
    }
}

using Api.Mappers;
using Api.Models;
using Api.Services.StatsService;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Stats>> Get(int id)
        {
            var stats = await _statsService.GetStatsByUserIdAsync(id);
            if (stats == null)
            {
                return NotFound();
            }
            return Ok(_statsMapper.MapStatsToDto(stats));
        }
        [HttpPut("{id}/salary")]
        public async Task<ActionResult> UpdateSalary(int id, [FromBody] int salary)
        {
            var stats = await _statsService.GetStatsByUserIdAsync(id);
            await _statsService.UpdateUserSalaryAsync(stats, salary);
            return Ok();
        }
    }
}

using Api.Models;
using Api.ResponseDTOs;

namespace Api.Mappers
{
    public class StatsMapper
    {
        public StatsDto MapStatsToDto(Stats stats)
        {
            if (stats is null)
            {
                return null;
            }

            return new StatsDto
            {
                Id = stats.UserId,
                DaysWorked = stats.DaysWorked,
                DaysPaid = stats.DaysPaid,
                Salary = stats.Salary
            };
        }
        public List<StatsDto> MapStatsListToDtoList(List<Stats> stats)
        {
            return stats.Select(MapStatsToDto).ToList();
        }
    }
}

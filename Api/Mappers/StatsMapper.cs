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
        public TeamStatsDto MapStatsToTeamStatsDto(List<Stats> stats)
        {
            if (stats is null)
            {
                return null;
            }

            return new TeamStatsDto
            {
                AverageSalary = stats.Average(s => s.Salary),
                HighestSalary = stats.Max(s => s.Salary),
                LowestSalary = stats.Min(s => s.Salary),
                TotalDaysPaid = stats.Sum(s => s.DaysPaid),
                TotalDaysWorked = stats.Sum(s => s.DaysWorked),
                TotalEarned = stats.Sum(s => s.DaysWorked * s.Salary),
                TotalDebt = stats.Sum(s => (s.DaysWorked * s.Salary) - (s.DaysPaid * s.Salary))
            };
        }
        public List<StatsDto> MapStatsListToDtoList(List<Stats> stats)
        {
            return stats.Select(MapStatsToDto).ToList();
        }
    }
}

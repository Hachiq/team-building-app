using Api.Models;

namespace Api.Services.StatsService
{
    public interface IStatsService
    {
        Task<Stats> GetStatsByUserIdAsync(int userId);
        Task UpdateUserSalaryAsync(Stats stats, int newSalary);
    }
}

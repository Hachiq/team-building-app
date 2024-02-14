using Api.Models;

namespace Api.Services.StatsService
{
    public interface IStatsService
    {
        Task<Stats> GetStatsByUserIdAsync(int userId);
    }
}

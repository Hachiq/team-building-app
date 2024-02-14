using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.StatsService
{
    public class StatsService : IStatsService
    {
        private readonly AppDbContext _db;
        public StatsService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<Stats> GetStatsByUserIdAsync(int userId)
        {
            return await _db.Stats.FirstOrDefaultAsync(s => s.UserId == userId);
        }
    }
}

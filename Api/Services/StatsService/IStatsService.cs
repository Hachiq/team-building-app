﻿using Api.Models;

namespace Api.Services.StatsService
{
    public interface IStatsService
    {
        Task<Stats> GetStatsByUserIdAsync(int userId);
        Task<List<Stats>> GetStatsByTeamIdAsync(int teamId);
        Task UpdateUserSalaryAsync(Stats stats, int newSalary);
        Task AddDayWorkedAsync(Stats stats);
        Task AddDayPaidAsync(Stats stats);
        Task ResetStatsOnDisbandAsync(Team team);
        Task ResetStatsOnLeaveAsync(Stats stats);
    }
}

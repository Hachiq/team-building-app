using Api.Models;

namespace Api.Services.TeamService
{
    public interface ITeamService
    {
        Task<List<Team>> GetTeamsAsync();
        Task<Team> GetTeamByNameAsync(string name);
        Task<Team> GetTeamByIdAsync(int id);
        Task<User> GetTeamLeaderAsync(Team team);
        Task CreateTeamAsync(Team team, User user);
        Task AddTeamMemberAsync(Team team, User user);
        Task DisbandTeamAsync(Team team);
        Task RemoveUserFromTeamAsync(Team team, User user);
    }
}

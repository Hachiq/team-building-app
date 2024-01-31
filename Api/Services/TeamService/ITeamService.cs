using Api.Models;

namespace Api.Services.TeamService
{
    public interface ITeamService
    {
        Task<List<Team>> GetTeamsAsync();
        Task<Team> GetTeamByNameAsync(string name);
        Task<Team> GetTeamByIdAsync(int id);
        Task CreateTeamAsync(Team team, User user);
    }
}

using Api.Models;

namespace Api.Services.TeamService
{
    public interface ITeamService
    {
        Task<List<Team>> GetTeamsAsync();
    }
}

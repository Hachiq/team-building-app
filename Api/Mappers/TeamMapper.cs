using Api.DTOs;
using Api.Models;

namespace Api.Mappers
{
    public class TeamMapper
    {
        public TeamDto MapTeamToDto(Team team)
        {
            if (team is null)
            {
                return null;
            }

            return new TeamDto
            {
                Name = team.Name,
                NumberOfMembers = team.Users.Count
            };
        }
        public List<TeamDto> MapTeamListToDtoList(List<Team> teams)
        {
            return teams.Select(MapTeamToDto).ToList();
        }
    }
}

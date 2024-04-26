using Api.Models;
using Api.ResponseDTOs;

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
                Id = team.Id,
                Name = team.Name,
                Leader = team.Users.SingleOrDefault(user => user.UserRoles.Any(userRole => userRole.RoleId == 2)).Username,
                NumberOfMembers = team.Users.Count
            };
        }
        public List<TeamDto> MapTeamListToDtoList(List<Team> teams)
        {
            return teams.Select(MapTeamToDto).ToList();
        }
    }
}

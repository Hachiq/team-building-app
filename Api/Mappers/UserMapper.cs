using Api.Models;
using Api.ResponseDTOs;

namespace Api.Mappers
{
    public class UserMapper
    {
        public UserDto MapUserToDto(User user)
        {
            if (user is null)
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                IsEmployed = user.Team != null
            };
        }
        public TeamMemberDto MapUserToTeamMemberDto(User user)
        {
            if (user is null)
            {
                return null;
            }

            return new TeamMemberDto
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Salary = user.Stats.Salary,
                Debt = (user.Stats.DaysWorked - user.Stats.DaysPaid) * user.Stats.Salary,
                IsLeader = user.UserRoles.Any(ur => ur.RoleId == 2)
            };
        }
        public List<UserDto> MapUserListToDtoList(List<User> users)
        {
            return users.Select(MapUserToDto).ToList();
        }
        public List<TeamMemberDto> MapUserListToTeamMemberDtoList(List<User> users)
        {
            return users.Select(MapUserToTeamMemberDto).ToList();
        }
    }
}

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
                Email = user.Email
            };
        }
        public List<UserDto> MapUserListToDtoList(List<User> users)
        {
            return users.Select(MapUserToDto).ToList();
        }
    }
}

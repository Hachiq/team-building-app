using Api.DTOs;
using Api.Models;

namespace Api.Mappers
{
    public class RequestMapper
    {
        public JoinRequestDto MapRequestToDto(Request request)
        {
            if (request is null)
            {
                return null;
            }

            return new JoinRequestDto
            {
                Id = request.Id,
                Username = request.User.Username,
                Date = request.Date,
                Status = request.Status.ToString()
            };
        }
        public List<JoinRequestDto> MapRequestListToDtoList(List<Request> requests)
        {
            return requests.Select(MapRequestToDto).ToList();
        }
    }
}

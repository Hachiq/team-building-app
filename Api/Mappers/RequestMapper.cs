using Api.Models;
using Api.ResponseDTOs;

namespace Api.Mappers
{
    public class RequestMapper
    {
        public RequestDto MapRequestToDto(Request request)
        {
            if (request is null)
            {
                return null;
            }

            return new RequestDto
            {
                Id = request.Id,
                Username = request.User.Username,
                Date = request.Date,
                Status = request.Status.ToString()
            };
        }
        public List<RequestDto> MapRequestListToDtoList(List<Request> requests)
        {
            return requests.Select(MapRequestToDto).ToList();
        }
    }
}

using Api.DTOs;
using Api.Models;

namespace Api.Services.RequestService
{
    public interface IRequestService
    {
        Task<Request> GetRequestByIdAsync(int id);
        Task<List<Request>> GetRequestsByTeamIdAsync(int teamId);
        Task CreateRequestAsync(int userId, int teamId);
        Task DeclineRequestAsync(Request request);
        Task AcceptRequestAsync(Request request);
        bool AlreadyProcessed(Request request);
        Task<bool> RequestIsSpam(CreateJoinRequestDto request);
    }
}

using Api.Data;
using Api.Models;
using Api.RequestDTOs;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.RequestService
{
    public class RequestService : IRequestService
    {
        private readonly AppDbContext _db;

        public RequestService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Request> GetRequestByIdAsync(int id)
        {
            return await _db.Requests.Include(r => r.User).FirstOrDefaultAsync(request => request.Id == id);
        }

        public async Task<List<Request>> GetRequestsByTeamIdAsync(int teamId)
        {
            return await _db.Requests.Include(r => r.User).Where(r => r.TeamId == teamId).ToListAsync();
        }

        public async Task CreateRequestAsync(int userId, int teamId)
        {
            var request = new Request
            {
                UserId = userId,
                TeamId = teamId,
                Date = DateTime.Now
            };
            await _db.AddAsync(request);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> RequestIsSpam(CreateRequestDto request)
        {
            var requestFromDb = await _db.Requests
                .FirstOrDefaultAsync(r =>
                    r.UserId == request.UserId &&
                    r.TeamId == request.TeamId &&
                    r.Status == RequestStatus.Pending);
            if (requestFromDb is null)
            {
                return false;
            }
            return true;
        }

        public async Task AcceptRequestAsync(Request request)
        {
            request.Accept();
            _db.Update(request);
            await _db.SaveChangesAsync();
        }

        public async Task DeclineRequestAsync(Request request)
        {
            request.Decline();
            _db.Update(request);
            await _db.SaveChangesAsync();
        }

        public async Task DeclineRequestsOnDisbandAsync(Team team)
        {
            foreach (var request in team.Requests)
            {
                request.Decline();
            }
            _db.Teams.Update(team);
            await _db.SaveChangesAsync();
        }

        public bool AlreadyProcessed(Request request)
        {
            return request.IsAccepted() || request.IsDeclined();
        }
    }
}

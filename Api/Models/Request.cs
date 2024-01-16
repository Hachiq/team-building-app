using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Request
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public User User { get; set; } = null!;
        public Team Team { get; set; } = null!;
        public RequestStatus Status { get; set; } = RequestStatus.Pending;

        public void Decline()
        {
            Status = RequestStatus.Declined;
        }
        public void Accept()
        {
            Status = RequestStatus.Accepted;
        }
        public bool IsDeclined()
        {
            return Status == RequestStatus.Declined;
        }
        public bool IsAccepted()
        {
            return Status == RequestStatus.Accepted;
        }
    }
    public enum RequestStatus
    {
        Pending,
        Accepted,
        Declined
    }
}

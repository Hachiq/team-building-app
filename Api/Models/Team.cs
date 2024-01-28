using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Team
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;
        public List<User> Users { get; set; } = new List<User>();
        public List<Request> Requests { get; set; } = new List<Request>();
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Stats
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public int DaysWorked { get; set; }
        public int DaysPaid { get; set; }
        public int Salary { get; set; }
        public User User { get; set; } = null!;
    }
}

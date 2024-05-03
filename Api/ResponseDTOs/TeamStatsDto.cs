namespace Api.ResponseDTOs
{
    public class TeamStatsDto
    {
        public double AverageSalary { get; set; }
        public int HighestSalary { get; set; }
        public int LowestSalary { get; set; }
        public int TotalDaysWorked { get; set; }
        public int TotalDaysPaid { get; set; }
        public int TotalEarned { get; set; }
        public int TotalDebt { get; set; }
    }
}

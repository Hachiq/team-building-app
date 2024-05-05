namespace Api.ResponseDTOs
{
    public class TeamMemberDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }
        public int Debt { get; set; }
        public bool IsLeader { get; set; }
    }
}

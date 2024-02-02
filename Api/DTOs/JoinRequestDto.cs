namespace Api.DTOs
{
    public class JoinRequestDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime Date  { get; set; }
        public string Status { get; set; }
    }
}

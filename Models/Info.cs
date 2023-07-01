namespace server.Model
{
    public class Info
    {
        public int Id { get; set; }
        public string? UserInformation { get; set; }
        public string? UserAddress { get; set; }
        public int TelephoneNumber { get; set; }
        public int Role { get; set; }
        public DateTime Date { get; set; }

        public Info()
        {
            Date = DateTime.UtcNow;
            Role = 0;
        }
    }
}
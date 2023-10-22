namespace ClientManagerDTO.Entity
{
    public class Client 
    {
        public Guid clientId { get; set; }

        public string rut { get; set; } = string.Empty;

        public string firstName { get; set; } = string.Empty;

        public string lastName { get; set; } = string.Empty;

        public bool married { get; set; } = false;

        public DateTime? dateOfBirth { get; set; } = DateTime.MinValue;

        public int age { get; set; } = 0;

        public string address { get; set; } = string.Empty;

        public string? phoneNumber { get; set; }

        public string email { get; set; } = string.Empty;

        public DateTime registerClient { get; set; } = DateTime.MinValue;
        public DateTime? updateClient { get; set; } = DateTime.MinValue;
    }
}

namespace RentalDeliverer.src.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Type { get; set; }
        public string Mail { get; set; }
        public string PasswordHash { get; set; }

        public Deliverer Deliverer { get; set; }
    }
}


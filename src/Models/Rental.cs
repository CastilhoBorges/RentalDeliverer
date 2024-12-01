namespace RentalDeliverer.src.Models
{
    public class Rental
    {
        public Guid RentalId { get; set; } 
        public Guid MotorcycleId { get; set; } 
        public Guid DelivererId { get; set; } 
        public Guid RentalTypeId { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public DateTime? EndDate { get; set; }


        public Motorcycle Motorcycle { get; set; }
        public Deliverer Deliverer { get; set; }
        public RentalType RentalType { get; set; }
    }
}

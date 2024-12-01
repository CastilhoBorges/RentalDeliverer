namespace RentalDeliverer.src.Models
{
    public class Motorcycle
    {
        public Guid MotorcycleId { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
    
        public ICollection<Rental> Rentals { get; set; }
    }
}
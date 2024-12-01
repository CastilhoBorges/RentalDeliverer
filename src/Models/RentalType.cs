﻿namespace RentalDeliverer.src.Models
{
    public class RentalType
    {
        public Guid RentalTypeId { get; set; } 
        public int Days { get; set; }
        public decimal Cost { get; set; }


        public ICollection<Rental> Rentals { get; set; }
    }
}
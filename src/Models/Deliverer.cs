﻿namespace RentalDeliverer.src.Models
{
    public class Deliverer
    {
        public Guid DelivererId { get; set; }
        public Guid UserId { get; set; }
        public string CNPJ { get; set; }
        public string CNH { get; set; }
        public string CNHType { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string CNHImgPath { get; set; }

        public User User { get; set; }
        public ICollection<Rental> Rentals { get; set; }
    }
}
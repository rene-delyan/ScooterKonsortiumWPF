using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterKonsortium {
    public class Company {
        public ICollection<Scooter> Scooters { get; set; } = new List<Scooter>(); //Inverse-Navigation Property
        public          int    Id        { get; set; }
        public required string Name      { get; set; }
        public          double CostPerKm { get; set; } = 0.59;
        public          string Email     { get; set; } = "";
        public          string Hotline   { get; set; } = "";
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterKonsortium {
    public class Chargingstation {
        public long                  Id        { get; set; }
        public required string       Name      { get; set; }
        public required int          PosX      { get; set; }
        public required int          PosY      { get; set; }
        public          int          Capacity  { get; set; } = 5;
        public          int          UsedSlots { get; set; }
        public ICollection <Scooter> Scooters  { get; set; } = new List<Scooter> (); //Inverse-Navigation Property
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterKonsortium {
    public enum ScooterState {
        Available,
        Charging
    }

    public class Scooter {
        public int              Id              { get; set; } //PK-Spalte
        public int              CompanyId       { get; set; } //FK-Spalte
        public int              PosX            { get; set; } = -1;
        public int              PosY            { get; set; } = -1;
        public int              Battery         { get; set; } = 100;
        public double           Revenue         { get; set; } = 0.00;
        public int              DistanceKm      { get; set; } = 0;
        public ScooterState     State           { get; set; } = ScooterState.Available;
        public long?            AtStationId     { get; set; } //optional
        public Company?         Company         { get; set; } = null; //Navigation Property
        public Chargingstation? Chargingstation { get; set; } = null; //Navigation Property
    }
}

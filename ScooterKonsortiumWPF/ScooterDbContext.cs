using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ScooterKonsortium {
    public class ScooterDbContext : DbContext {
        public DbSet <Scooter>         Scooters         { get; set; }
        public DbSet <Company>         Companies        { get; set; }
        public DbSet <Chargingstation> ChargingStations { get; set; }

        // Configuration der Datenbankverbindung
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite ("Data Source=scooterKonsortium.db");
        }

        //Setzt die Primärschlüssel und Defaultwerte für die Entitäten
        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.Entity <Company> (entity => {
                entity.HasKey   (c => c.Id);
                
                entity.Property (c => c.Name).IsRequired ();
                entity.HasIndex (c => c.Name).IsUnique   ();

                entity.Property (c => c.CostPerKm).HasDefaultValue (0.59);
            });

            modelBuilder.Entity <Chargingstation> (entity => {
                entity.HasKey   (cs => cs.Id);
                
                entity.Property (cs => cs.Name).IsRequired ();
                entity.Property (cs => cs.PosX).IsRequired ().    HasDefaultValue (-1);
                entity.Property (cs => cs.PosY).IsRequired ().    HasDefaultValue (-1);
                entity.Property (cs => cs.Capacity).IsRequired ().HasDefaultValue (5);
            });

            modelBuilder.Entity <Scooter> (entity => {
                entity.HasKey   (s => s.Id);
                entity.Property (s => s.PosX).HasDefaultValue (-1);
                entity.Property (s => s.PosY).HasDefaultValue (-1);
                entity.Property (s => s.Battery).HasDefaultValue (100);
                entity.Property (s => s.Revenue).HasDefaultValue (0.00);
                entity.Property (s => s.DistanceKm).HasDefaultValue (0);
                entity.Property (s => s.State).HasDefaultValue (ScooterState.Available);

                entity.HasOne (s => s.Company).           // Beziehung zu Company
                       WithMany (c => c.Scooters).
                       HasForeignKey (s => s.CompanyId).
                       OnDelete (DeleteBehavior.Cascade);

                entity.HasOne (s => s.Chargingstation).   // Beziehung zu Chargingstation
                       WithMany (cs => cs.Scooters).
                       HasForeignKey (s => s.AtStationId).
                       IsRequired (false);
            });
        }
    }
}

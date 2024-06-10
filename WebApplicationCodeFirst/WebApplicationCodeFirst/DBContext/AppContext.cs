using WebApplicationCodeFirst.Models;

namespace WebApplicationCodeFirst.DBContext;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext


{
    protected ApplicationDbContext()
    {
    }

      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription_Medicament> PrescriptionMedicaments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Prescription_Medicament>()
                .HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });

            // Seed initial data
            modelBuilder.Entity<Doctor>().HasData(new List<Doctor>()
            {
                new() { IdDoctor = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new() { IdDoctor = 2, FirstName = "Ann", LastName = "Smith", Email = "ann.smith@example.com" },
                new() { IdDoctor = 3, FirstName = "Jack", LastName = "Taylor", Email = "jack.taylor@example.com" }
            });

            modelBuilder.Entity<Patient>().HasData(new List<Patient>()
            {
                new() { IdPatient = 1, FirstName = "Jane", LastName = "Doe", Birthdate = new DateTime(1990, 1, 1) },
                new() { IdPatient = 2, FirstName = "Bob", LastName = "Johnson", Birthdate = new DateTime(1985, 2, 20) },
                new() { IdPatient = 3, FirstName = "Alice", LastName = "Williams", Birthdate = new DateTime(2000, 3, 15) }
            });

            modelBuilder.Entity<Medicament>().HasData(new List<Medicament>()
            {
                new() { IdMedicament = 1, Name = "Aspirin", Description = "Pain reliever", Type = "Tablet" },
                new() { IdMedicament = 2, Name = "Penicillin", Description = "Antibiotic", Type = "Injection" },
                new() { IdMedicament = 3, Name = "Ibuprofen", Description = "Anti-inflammatory", Type = "Tablet" }
            });

            modelBuilder.Entity<Prescription>().HasData(new List<Prescription>()
            {
                new() { IdPrescription = 1, Date = DateTime.Now, DueDate = DateTime.Now.AddMonths(1), IdPatient = 1, IdDoctor = 1 },
                new() { IdPrescription = 2, Date = DateTime.Now, DueDate = DateTime.Now.AddMonths(1), IdPatient = 2, IdDoctor = 2 },
                new() { IdPrescription = 3, Date = DateTime.Now, DueDate = DateTime.Now.AddMonths(1), IdPatient = 3, IdDoctor = 3 }
            });

            modelBuilder.Entity<Prescription_Medicament>().HasData(new List<Prescription_Medicament>()
            {
                new() { IdMedicament = 1, IdPrescription = 1, Dose = 1, Details = "Take one tablet daily" },
                new() { IdMedicament = 2, IdPrescription = 2, Dose = 2, Details = "Inject twice daily" },
                new() { IdMedicament = 3, IdPrescription = 3, Dose = 3, Details = "Take three tablets daily" }
            });
        }
    
}

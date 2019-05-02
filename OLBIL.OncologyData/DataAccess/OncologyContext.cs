using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OLBIL.OncologyDomain.Entities;
using OLBIL.OncologyData.Mappings;
using OLBIL.OncologyApplication.Interfaces;

namespace OLBIL.OncologyData
{
    public class OncologyContext : DbContext, IOncologyContext
    {
        public OncologyContext(DbContextOptions<OncologyContext> options)
            : base(options)
        {
            
        }

        public DbSet<MedicalSpecialty> MedicalSpecialties { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<AdministrativeDivision> AdministrativeDivisions { get; set; }
        public DbSet<OncologyPatient> OncologyPatients { get; set; }
        public DbSet<PatientPhysicalRecord> PatientPhysicalRecords { get; set; }
        public DbSet<EvolutionCard> EvolutionCards { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<HealthProfessional> HealthProfessionals { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<HospitalUnit> HospitalUnits { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<AppointmentReason> AppointmentReasons { get; set; }
        public DbSet<AmbulatoryAttentionRecord> AmbulatoryAttentionRecords { get; set; }
        public DbSet<RecordStorageLocation> RecordStorageLocations { get; set; }
        public DbSet<PhysicalRecordTransfer> PhysicalRecordTransfers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w => w.Throw(RelationalEventId.QueryClientEvaluationWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("olbil");

            foreach(var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.ClrType.Name.ToLower();

                foreach (var prop in entity.GetProperties())
                {
                    prop.Relational().ColumnName = prop.Relational().ColumnName.ToLower();
                }
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonTypeConfiguration).Assembly);
        }
    }
}

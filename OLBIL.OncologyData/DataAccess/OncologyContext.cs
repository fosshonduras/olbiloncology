using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OLBIL.OncologyCore.Entities;
using OLBIL.OncologyData.Mappings;

namespace OLBIL.OncologyData
{
    public class OncologyContext : DbContext
    {
        public OncologyContext(DbContextOptions<OncologyContext> options)
            : base(options)
        {
            
        }

        public DbSet<OncologyPatient> OncologyPatients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<HealthProfessional> HealthProfessionals { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<HospitalUnit> Units { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Bed> Beds { get; set; }

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

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OncologyPatientTypeConfiguration).Assembly);
        }
    }
}

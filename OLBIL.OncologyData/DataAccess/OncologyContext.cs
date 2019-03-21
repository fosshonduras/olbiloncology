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


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w => w.Throw(RelationalEventId.QueryClientEvaluationWarning));
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OncologyPatientTypeConfiguration).Assembly);
            foreach(var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.Relational().TableName.ToLower();

                foreach (var prop in entity.GetProperties())
                {
                    prop.Relational().ColumnName = prop.Relational().ColumnName.ToLower();
                }
            }
            
        }
    }
}

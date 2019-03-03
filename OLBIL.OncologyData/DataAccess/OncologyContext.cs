using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OLBIL.OncologyCore.Entities;

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
            modelBuilder.Entity<Person>().ToTable("Person");
        }
    }
}

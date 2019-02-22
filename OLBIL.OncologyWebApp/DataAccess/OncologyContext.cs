using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyWebApp.Entities;

namespace OLBIL.OncologyWebApp.DataAccess
{
    public class OncologyContext : DbContext
    {
        public OncologyContext(DbContextOptions<OncologyContext> options)
            : base(options)
        {

        }

        public DbSet<OncologyPatient> OncologyPatients { get; set; }

    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLBIL.OncologyCore.Entities;

namespace OLBIL.OncologyData.Mappings
{
    public class OncologyPatientTypeConfiguration : IEntityTypeConfiguration<OncologyPatient>
    {
        public void Configure(EntityTypeBuilder<OncologyPatient> builder)
        {
            //Table
            builder.ToTable("oncologypatient", "olbil");

            //Primary Key
            builder.HasKey(u => u.OncologyPatientId);
        }
    }
}

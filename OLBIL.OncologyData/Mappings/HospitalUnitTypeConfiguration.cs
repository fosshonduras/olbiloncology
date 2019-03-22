using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLBIL.OncologyCore.Entities;

namespace OLBIL.OncologyData.Mappings
{
    public class HospitalUnitTypeConfiguration : IEntityTypeConfiguration<HospitalUnit>
    {
        public void Configure(EntityTypeBuilder<HospitalUnit> builder)
        {
            builder.ToTable("unit", "olbil");
            builder.HasKey(h => h.UnitId);
        }
    }
}

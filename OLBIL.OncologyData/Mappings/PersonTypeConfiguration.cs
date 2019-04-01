using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLBIL.OncologyDomain.Entities;

namespace OLBIL.OncologyData.Mappings
{
    public class PersonTypeConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            //Table
            builder.ToTable("person", "olbil");

            //Primary Key
            builder.HasKey(u => u.PersonId);
        }
    }
}

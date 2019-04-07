using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyData;
using OLBIL.OncologyDomain.Entities;
using System;

namespace OLBIL.OncologyTests.Utils
{
    public class OncologyContextFactory
    {
        public static OncologyContext Create()
        {
            var options = new DbContextOptionsBuilder<OncologyContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new OncologyContext(options);

            context.Database.EnsureCreated();

            context.People.AddRange(new[] {
                new Person { FirstName = "Kevin", LastName = "Cordoba" },
                new Person { FirstName = "Jimmy", LastName = "Torres" },
                new Person { FirstName = "Brenda", LastName = "Recarte" },
            });

            context.SaveChanges();

            return context;
        }

        public static void Destroy(OncologyContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}

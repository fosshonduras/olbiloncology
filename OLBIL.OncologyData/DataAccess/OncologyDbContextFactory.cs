using Microsoft.EntityFrameworkCore;

namespace OLBIL.OncologyData.DataAccess
{
    public class OncologyDbContextFactory : DesignTimeDbContextFactoryBase<OncologyContext>
    {
        protected override OncologyContext CreateNewInstance(DbContextOptions<OncologyContext> options)
        {
            return new OncologyContext(options);
        }
    }
}

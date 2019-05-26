using OLBIL.Common;
using System;

namespace OLBIL.OncologyInfrastructure
{
    public class SystemDateTime : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}

using OLBIL.Common;
using System;

namespace OLBIL.OncologyInfrastructure
{
    public class SystemDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}

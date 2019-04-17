using OLBIL.Core;
using System;

namespace OLBIL.OncologyCrossCutting
{
    public class SystemDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}

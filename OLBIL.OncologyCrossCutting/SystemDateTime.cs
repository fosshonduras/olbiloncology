using OLBIL.Common;
using System;

namespace OLBIL.OncologyInfrastructure
{
    public class SystemDateTime : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;

        public Tuple<int, int, int> CalculateDifference(DateTime pastDate, DateTime currentDate)
        {
            var ageInDays = (currentDate.Date - pastDate.Date).Days;
            var ageInMonths = ageInDays / 30;
            ageInDays = ageInDays % 30;
            var ageInYears = ageInMonths / 12;
            ageInMonths = ageInMonths % 12;

            return new Tuple<int, int, int>(ageInYears, ageInMonths, ageInDays);
        }
    }
}

using OLBIL.Common;
using System;

namespace OLBIL.OncologyCrossCutting
{
    public class DateTimeCalculationsDomainService : IDateTimeCalculationsDomainService
    {
        public AgeDescriptor CalculateDifference(DateTime pastDate, DateTime currentDate)
        {
            var ageInDays = (currentDate.Date - pastDate.Date).Days;
            var ageInMonths = ageInDays / 30;
            ageInDays = ageInDays % 30;
            var ageInYears = ageInMonths / 12;
            ageInMonths = ageInMonths % 12;

            return new AgeDescriptor(ageInYears, ageInMonths, ageInDays);
        }
    }
}

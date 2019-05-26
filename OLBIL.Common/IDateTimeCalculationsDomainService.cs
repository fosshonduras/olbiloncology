using System;

namespace OLBIL.Common
{
    public interface IDateTimeCalculationsDomainService
    {
        AgeDescriptor CalculateDifference(DateTime pastDate, DateTime currentDate);
    }
}

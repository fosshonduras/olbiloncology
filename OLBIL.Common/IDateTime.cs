using System;

namespace OLBIL.Common
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
        Tuple<int, int, int> CalculateDifference(DateTime pastDate, DateTime currentDate);
    }
}

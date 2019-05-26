using System;

namespace OLBIL.Common
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}

using MobileStatisticsApp.Core.Entities;
using static MobileStatisticsApp.Test.MobileStatisticsIntegrationTests;

namespace MobileStatisticsApp.Test;

public static class DateExtensions
{
    public static bool EqualWithoutHours(this DateTime dt1, DateTime dt2)
    {
        return dt1.TrimToHours() == dt2.TrimToHours();
    }

    private static DateTime TrimToHours(this DateTime dt)
    {
        return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0, 0);
    }
}
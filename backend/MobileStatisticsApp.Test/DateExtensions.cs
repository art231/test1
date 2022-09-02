namespace MobileStatisticsApp.Test;
/// <summary>
/// Расширение для тестов.
/// </summary>
public static class DateExtensions
{
    /// <summary>
    /// Расширение для времени.
    /// </summary>
    /// <param name="dt1">1 Параметр сравнения.</param>
    /// <param name="dt2">2 Параметр сравнения.</param>
    /// <returns></returns>
    public static bool EqualWithoutHours(this DateTime dt1, DateTime dt2)
    {
        return dt1.TrimToHours() == dt2.TrimToHours();
    }

    private static DateTime TrimToHours(this DateTime dt)
    {
        return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0, 0);
    }
}
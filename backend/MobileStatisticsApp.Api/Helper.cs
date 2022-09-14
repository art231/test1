namespace MobileStatisticsApp.Api;

/// <summary>
/// Хелпер.
/// </summary>
public static class Helper
{
    /// <summary>
    /// Для конвертации времени в iso.
    /// </summary>
    /// <param name="date"> Параметр.</param>
    /// <returns>Строка даты.</returns>
    public static string ConvertToIso(DateTime date) => date.ToString("yyyy-mm-ddThh:mm:ss.fffZ");
}

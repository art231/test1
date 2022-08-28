namespace MobileStatisticsApp.Api;

public static class Helper
{
    public static string ConvertToIso(DateTime date)
    {
        return date.ToString("yyyy-mm-ddThh:mm:ss.fffZ");
    }
}

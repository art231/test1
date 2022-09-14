namespace MobileStatisticsApp.Infrastructure;
/// <summary>
/// Расширение для таблиц.
/// </summary>
public static class RepositoryExtensions
{
    /// <summary>
    /// Корректное чтение таблиц бд.
    /// </summary>
    public static void AddUnderScores()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }
}
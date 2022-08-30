using MobileStatisticsApp.Application.Repositories;
using MobileStatisticsApp.Repositories;

namespace MobileStatistics.Application;

/// <summary>
///     Доступ к репозитариям.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///     Репозитарий мобильной статистики.
    /// </summary>
    IMobileStatisticsRepository MobileStatisticsRepository { get; }
    /// <summary>
    ///     Репозитарий событий мобильной статистики.
    /// </summary>
    IMobileStatisticsEventsRepository MobileStatisticsEventsRepository { get; }

    void Commit();
}
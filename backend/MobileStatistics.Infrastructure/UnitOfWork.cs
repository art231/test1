using MobileStatistics.Application;
using MobileStatisticsApp.Application.Repositories;
using MobileStatisticsApp.Repositories;

namespace MobileStatisticsApp.Infrastructure;

/// <summary>
///     Объединения репозитариев.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    /// <summary>
    ///     Конструктор объединений.
    /// </summary>
    /// <param name="mobileStatisticsRepository">Репозиторий мобильной статистики.</param>
    ///<param name="mobileStatisticsEventsRepository">Репозиторий мобильной статистики.</param>
    public UnitOfWork(IMobileStatisticsRepository mobileStatisticsRepository,
        IMobileStatisticsEventsRepository mobileStatisticsEventsRepository)
    {
        MobileStatisticsRepository = mobileStatisticsRepository;
        MobileStatisticsEventsRepository = mobileStatisticsEventsRepository;
    }

    /// <summary>
    ///     Репозиторий мобильной статистики.
    /// </summary>
    public IMobileStatisticsRepository MobileStatisticsRepository { get; }
    /// <summary>
    ///     Репозиторий событий мобильной статистики.
    /// </summary>
    public IMobileStatisticsEventsRepository MobileStatisticsEventsRepository { get; }
}
using MobileStatistics.Application;
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
    public UnitOfWork(IMobileStatisticsRepository mobileStatisticsRepository)
    {
        MobileStatisticsRepository = mobileStatisticsRepository;
    }

    /// <summary>
    ///     Репозиторий мобильной статистики.
    /// </summary>
    public IMobileStatisticsRepository MobileStatisticsRepository { get; }
}
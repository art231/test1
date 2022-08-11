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
}
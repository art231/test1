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
    /// <summary>
    /// Завершение транзакции.
    /// </summary>
    void Commit();
    /// <summary>
    /// Очистка данных транзакции.
    /// </summary>
    void Dispose();
    /// <summary>
    /// Коммит и очистка.
    /// </summary>
    void CommitAndDispose();
}
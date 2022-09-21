//using MobileStatisticsApp.Application.Repositories;
using MobileStatisticsApp.Repositories;
using System.Data;

namespace MobileStatistics.Application;

/// <summary>
///     Доступ к репозитариям.
/// </summary>
//public interface IUnitOfWork
//{
//    /// <summary>
//    ///     Репозитарий мобильной статистики.
//    /// </summary>
//    IMobileStatisticsRepository MobileStatisticsRepository { get; }
//    /// <summary>
//    ///     Репозитарий событий мобильной статистики.
//    /// </summary>
//    IMobileStatisticsEventsRepository MobileStatisticsEventsRepository { get; }
//    /// <summary>
//    /// Завершение транзакции.
//    /// </summary>
//    void Commit();
//}
public interface IUnitOfWork : IDisposable
{
    IDbConnection Connection { get; }
    IDbTransaction Transaction { get; }
    void Begin();
    void Commit();
    void Rollback();
}
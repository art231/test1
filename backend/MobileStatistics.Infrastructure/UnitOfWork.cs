using System.Data;
using MobileStatistics.Application;
using MobileStatisticsApp.Application.Repositories;
using MobileStatisticsApp.Repositories;

namespace MobileStatisticsApp.Infrastructure;

/// <summary>
///     Объединения репозитариев.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private IDbTransaction dbTransaction;

    /// <summary>
    ///     Конструктор объединений.
    /// </summary>
    /// <param name="mobileStatisticsRepository">Репозиторий мобильной статистики.</param>
    ///<param name="mobileStatisticsEventsRepository">Репозиторий мобильной статистики.</param>
    public UnitOfWork(IDbTransaction dbTransaction, IMobileStatisticsRepository mobileStatisticsRepository,
        IMobileStatisticsEventsRepository mobileStatisticsEventsRepository)
    {
        MobileStatisticsRepository = mobileStatisticsRepository;
        MobileStatisticsEventsRepository = mobileStatisticsEventsRepository;
        this.dbTransaction = dbTransaction;
    }

    /// <summary>
    ///     Репозиторий мобильной статистики.
    /// </summary>
    public IMobileStatisticsRepository MobileStatisticsRepository { get; }

    /// <summary>
    ///     Репозиторий событий мобильной статистики.
    /// </summary>
    public IMobileStatisticsEventsRepository MobileStatisticsEventsRepository { get; }

    public void Commit()
    {
        try
        {
            dbTransaction.Commit();
            // Добавив это, мы можем иметь несколько транзакций как часть одного запроса.
            dbTransaction.Connection.BeginTransaction();
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
        }
    }
}
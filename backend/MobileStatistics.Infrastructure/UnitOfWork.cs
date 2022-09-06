using System.Data;
using MobileStatistics.Application;
using MobileStatisticsApp.Application.Repositories;
using MobileStatisticsApp.Repositories;

namespace MobileStatisticsApp.Infrastructure;

/// <summary>
///     Объединения репозитариев.
/// </summary>
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private IDbTransaction dbTransaction;

    /// <summary>
    ///     Конструктор объединений.
    /// </summary>
    /// <param name="dbTransaction">Параметр транзакции.</param>
    /// <param name="mobileStatisticsRepository">Репозиторий мобильной статистики.</param>
    ///<param name="mobileStatisticsEventsRepository">Репозиторий мобильной статистики.</param>
    public UnitOfWork(IDbTransaction dbTransaction,
        IMobileStatisticsRepository mobileStatisticsRepository,
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
    
    /// <summary>
    /// Добавление транзакции.
    /// </summary>
    public void Commit()
    {
        try
        {
            dbTransaction.Commit();
        }
        catch (Exception)
        {
            dbTransaction.Rollback();
        }
    }
    /// <summary>
    /// Коммит и очистка памяти.
    /// </summary>
    public void CommitAndDispose()
    {
        Commit();
        Dispose();
    }
    /// <summary>
    /// Очистка памяти.
    /// </summary>
    public void Dispose()
    {
        dbTransaction.Dispose();
    }
}
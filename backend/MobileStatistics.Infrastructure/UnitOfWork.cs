using System.Data;
using MobileStatistics.Application;

namespace MobileStatisticsApp.Infrastructure;

/// <summary>
/// Модель подключения данных.
/// </summary>
public interface IDalSession
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    UnitOfWork UnitOfWork { get; }
}

/// <summary>
/// Модель подключения данных.
/// </summary>
public class DalSession : IDalSession, IDisposable
{
    private IDbConnection dbConnection;
    private UnitOfWork unitOfWork;
    /// <summary>
    /// Открытие сессии подключения.
    /// </summary>
    /// <param name="dbConnection">Подключение к бд.</param>
    public DalSession(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
        this.dbConnection.Open();
        unitOfWork = new UnitOfWork(dbConnection);
    }

    /// <summary>
    /// Модель объединения для транзакции.
    /// </summary>
    public UnitOfWork UnitOfWork => unitOfWork;
    /// <summary>
    /// Очистка памяти.
    /// </summary>
    public void Dispose()
    {
        unitOfWork.Dispose();
        dbConnection.Dispose();
    }
}

/// <summary>
/// Юнит оф ворк.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly IDbConnection connection;
    private IDbTransaction? transaction;
    /// <summary>
    /// Конструктор Юнит оф ворк.
    /// </summary>
    /// <param name="connection">Подключение.</param>
    public UnitOfWork(IDbConnection connection)
    {
        this.connection = connection;
    }

    /// <summary>
    /// Подключение.
    /// </summary>
    IDbConnection IUnitOfWork.Connection => connection;

    /// <summary>
    /// Подключение транзакции.
    /// </summary>
    IDbTransaction IUnitOfWork.Transaction => transaction!;

    /// <summary>
    /// Начало транзакции.
    /// </summary>
    public void Begin()
    {
        transaction = connection.BeginTransaction();
    }

    /// <summary>
    /// Коммит.
    /// </summary>
    public void Commit()
    {
        transaction?.Commit();
        Dispose();
    }

    /// <summary>
    /// Откат.
    /// </summary>
    public void Rollback()
    {
        transaction?.Rollback();
        Dispose();
    }

    /// <summary>
    /// Очистка памяти.
    /// </summary>
    public void Dispose()
    {
        if (transaction != null) transaction.Dispose();
        transaction = null;
    }
}
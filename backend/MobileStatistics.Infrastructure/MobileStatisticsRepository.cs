using Dapper;
using MobileStatisticsApp.Core.Entities;
using MobileStatisticsApp.Repositories;
using System.Data;

namespace MobileStatisticsApp.Infrastructure;

/// <summary>
/// Репозитарий мобильной статистики.
/// </summary>
public class MobileStatisticsRepository : IMobileStatisticsRepository
{

    private readonly IDbTransaction dbTransaction;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="dbTransaction">Параметр транзакции.</param>
    public MobileStatisticsRepository(
        IDbTransaction dbTransaction)
    {
        RepositoryExtensions.AddUnderScores();
        this.dbTransaction = dbTransaction;
    }

    /// <summary>
    /// Добавление новой сущности.
    /// </summary>
    /// <param name="entity">Новая сущность.</param>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    public async Task AddAsync(MobileStatisticsItem entity)
    {
        entity.Id = Guid.NewGuid();
        var sql =
            @"INSERT INTO mobile_statistics ( id, title, last_statistics, version_client, type)
                VALUES(@Id, @Title, @LastStatistics, @VersionClient, @Type);";
        await dbTransaction.Connection.ExecuteAsync(sql, entity, dbTransaction);
    }

    /// <summary>
    /// Получение всего списка.
    /// </summary>
    /// <returns>Весь список.</returns>
    public async Task<IReadOnlyList<MobileStatisticsItem>> GetAllAsync()
    {
        var sql =
            @"SELECT * FROM mobile_statistics";
        var result = await dbTransaction.Connection.QueryAsync<MobileStatisticsItem>(sql, dbTransaction);

        return result.ToList();
    }

    /// <summary>
    /// Получение по ключу объекта.
    /// </summary>
    /// <param name="id">Уникальный идентификатор.</param>
    /// <returns>объект.</returns>
    public async Task<MobileStatisticsItem> GetByIdAsync(Guid id)
    {
        var sql =
            @"SELECT * FROM mobile_statistics where Id=@Id";
        return await dbTransaction.Connection.QuerySingleOrDefaultAsync<MobileStatisticsItem>(sql, new { Id = id }, dbTransaction);
    }

    /// <summary>
    /// Обновление объекта.
    /// </summary>
    /// <param name="entity">Объект для изменения.</param>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    public async Task UpdateAsync(MobileStatisticsItem entity)
    {
        var sql =
            @"UPDATE mobile_statistics SET title = @Title,
                last_statistics = @LastStatistics,  
                version_client = @VersionClient,
                type = @Type where id=@Id";
        await dbTransaction.Connection.ExecuteAsync(sql, entity, dbTransaction);
    }
}
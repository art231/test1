using System.Data;
using Dapper;
using MobileStatisticsApp.Application.Repositories;
using MobileStatisticsApp.Core.Entities;

namespace MobileStatisticsApp.Infrastructure;

/// <summary>
/// Репозитарий событий мобильной статистики.
/// </summary>
public class MobileStatisticsEventsRepository : IMobileStatisticsEventsRepository
{
    private readonly IDbTransaction dbTransaction;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="dbTransaction"> Параметр транзакции.</param>
    public MobileStatisticsEventsRepository(
        IDbTransaction dbTransaction)
    {
        RepositoryExtensions.AddUnderScores();
        this.dbTransaction = dbTransaction;
    }

    /// <summary>
    /// Добавление нового события.
    /// </summary>
    /// <param name="entities">Новая сущность.</param>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    public async Task CreateEventsAsync(IEnumerable<MobileStatisticsEvent> entities)
    {
        var sql =
            @"INSERT INTO mobile_statistics_events (mobile_statistics_id, id, Name, Date, description)
                VALUES(@MobileStatisticsId, @Id, @Name, @Date, @Description);";
        await dbTransaction.Connection.ExecuteAsync(sql, entities, dbTransaction);
    }

    /// <summary>
    /// Получение по ключу объекта.
    /// </summary>
    /// <param name="mobileStatisticsId">Уникальный идентификатор.</param>
    /// <returns>Объект событий.</returns>
    public async Task<IEnumerable<MobileStatisticsEvent>> GetByIdAsync(Guid mobileStatisticsId)
    {
        var sql =
            @"SELECT  id, mobile_statistics_id as MobileStatisticsId, date, name, description 
                FROM mobile_statistics_events where mobile_statistics_id = @MobileStatisticsId";
        return await dbTransaction.Connection.QueryAsync<MobileStatisticsEvent>(sql,
            new { MobileStatisticsId = mobileStatisticsId }, dbTransaction);
    }
    /// <summary>
    /// Удаление события мобильной статистики.
    /// </summary>
    /// <param name="mobileStatisticsEventId">Ключ для удаления.</param>
    /// <returns>Выполнение удаления события.</returns>
    public async Task DeleteAsync(Guid mobileStatisticsEventId)
    {
        var sql =
            @"DELETE FROM public.mobile_statistics_events
	            WHERE id = @MobileStatisticsEventId;";
        await dbTransaction.Connection.ExecuteAsync(sql, new { MobileStatisticsEventId = mobileStatisticsEventId }, dbTransaction);
    }
}
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
    /// Обновление описания события.
    /// </summary>
    /// <param name="entity">Модель для обновления.</param>
    /// <returns>Выпонление события.</returns>
    public async Task UpdateEventAsync(MobileStatisticsEvent entity)
    {
        var sql =
            @"UPDATE public.mobile_statistics_events
	            SET description=@Description
	            WHERE id=@Id";
        await dbTransaction.Connection.ExecuteAsync(sql, entity, dbTransaction);
    }

    /// <summary>
    /// Получение по ключу объекта.
    /// </summary>
    /// <param name="mobileStatisticsId">Уникальный идентификатор.</param>
    /// <returns>Объект событий.</returns>
    public async Task<IEnumerable<MobileStatisticsEvent>> GetListEventsByIdAsync(Guid mobileStatisticsId)
    {
        var sql =
            @"SELECT  id, mobile_statistics_id as MobileStatisticsId, date, name, description 
                FROM mobile_statistics_events where mobile_statistics_id = @MobileStatisticsId";
        return await dbTransaction.Connection.QueryAsync<MobileStatisticsEvent>(sql,
            new { MobileStatisticsId = mobileStatisticsId }, dbTransaction);
    }
    /// <summary>
    /// Получение события мобильной статистики.
    /// </summary>
    /// <param name="mobileStatisticsEventId">Ключ события.</param>
    /// <returns>Событие.</returns>
    public async Task<MobileStatisticsEvent> GetEventByIdAsync(Guid mobileStatisticsEventId)
    {
        var sql =
            @"SELECT  id, mobile_statistics_id as MobileStatisticsId, date, name, description 
                FROM mobile_statistics_events where id = @MobileStatisticsEventId";
        return await dbTransaction.Connection.QuerySingleOrDefaultAsync<MobileStatisticsEvent>(sql,
            new { MobileStatisticsEventId = mobileStatisticsEventId }, dbTransaction);
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
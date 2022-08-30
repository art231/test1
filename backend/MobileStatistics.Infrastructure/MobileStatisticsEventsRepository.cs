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
    /// <summary>
    /// Подключение базы данных.
    /// </summary>
    private readonly IDbConnection dbconnection;

    private readonly IDbTransaction dbTransaction;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="dbconnection">Соединение.</param>
    public MobileStatisticsEventsRepository(IDbConnection dbconnection,
        IDbTransaction dbTransaction)
    {
        this.dbconnection = dbconnection;
        this.dbTransaction = dbTransaction;
    }

    /// <summary>
    /// Добавление нового события.
    /// </summary>
    /// <param name="entities">Новая сущность.</param>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    public async Task CreateEventsAsync(IEnumerable<MobileStatisticsEvent> entities)
    {
        foreach (var entity in entities) entity.Id = Guid.NewGuid();
        var sql =
            @"INSERT INTO mobile_statistics_events (mobile_statistics_id, id, Name, Date, description)
            VALUES(@MobileStatisticsId, @Id, @Name, @Date, @Description);";
        await dbconnection.ExecuteAsync(sql, entities, dbTransaction);
    }

    /// <summary>
    /// Получение по ключу объекта.
    /// </summary>
    /// <param name="mobileStatisticsId">Уникальный идентификатор.</param>
    /// <returns>Объект событий.</returns>
    public async Task<IEnumerable<MobileStatisticsEvent>> GetByIdAsync(Guid mobileStatisticsId)
    {
        var sql =
            @"SELECT * FROM mobile_statistics_events where mobile_statistics_id = @MobileStatisticsId";
        return await dbconnection.QueryAsync<MobileStatisticsEvent>(sql,
            new { MobileStatisticsId = mobileStatisticsId }, dbTransaction);
    }
}
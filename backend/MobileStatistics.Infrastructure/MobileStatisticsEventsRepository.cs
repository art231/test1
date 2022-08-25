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

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="dbconnection">Соединение.</param>
    public MobileStatisticsEventsRepository(IDbConnection dbconnection)
    {
        this.dbconnection = dbconnection;
    }

    /// <summary>
    /// Добавление нового события.
    /// </summary>
    /// <param name="entities">Новая сущность.</param>
    public void CreateEvent(IEnumerable<MobileStatisticsEvent> entities)
    {
        dbconnection.Open();
        try
        {
            foreach (var entity in entities)
            {
                entity.Id = Guid.NewGuid();
            }
            var sql =
                @"INSERT INTO mobile_statistics_events (mobile_statistics_id, id, Name, Date, description)
                VALUES(@MobileStatisticsId, @Id, @Name, @Date, @Description);";
            dbconnection.Execute(sql, entities);
        }
        finally
        {
            dbconnection.Close();
        }
    }

    /// <summary>
    /// Получение по ключу объекта.
    /// </summary>
    /// <param name="eventId">Уникальный идентификатор.</param>
    /// <returns>Объект событий.</returns>
    public async Task<IEnumerable<MobileStatisticsEvent>> GetByIdAsync(Guid eventId)
    {
        dbconnection.Open();
        try
        {
            var sql =
                @"SELECT * FROM mobile_statistics_events where mobile_statistics_id=@Id";
            return await dbconnection.QueryAsync<MobileStatisticsEvent>(sql, new { Id = eventId });
        }
        finally
        {
            dbconnection.Close();
        }
    }
}
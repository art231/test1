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
    /// <param name="entity">Новая сущность.</param>
    public void CreateEvent(MobileStatisticsEvents entity)
    {
        dbconnection.Open();
        try
        {
            entity.Id = Guid.NewGuid();
            var sql =
                @"INSERT INTO mobile_statistics_events (mobile_statistics_id, id, Name, Date, description)
                VALUES(@MobileStatisticsId, @Id, @Name, @Date, @Description);";
            dbconnection.ExecuteAsync(sql, entity);
        }
        finally
        {
            dbconnection.Close();
        }
    }

    /// <summary>
    /// Получение по ключу объекта.
    /// </summary>
    /// <param name="id">Уникальный идентификатор.</param>
    /// <returns>Объект событий.</returns>
    public async Task<IEnumerable<MobileStatisticsEvents>> GetByIdAsync(Guid id)
    {
        try
        {
            var sql =
                @"SELECT * FROM mobile_statistics_events where mobile_statistics_id=@Id";
            return await dbconnection.QueryAsync<MobileStatisticsEvents>(sql, new { Id = id });
        }
        finally
        {
            dbconnection.Close();
        }
    }

    /// <summary>
    /// Обновление объекта.
    /// </summary>
    /// <param name="entity">Объект для изменения.</param>
    public void Update(MobileStatisticsEvents entity)
    {
        dbconnection.Open();
        try
        {
            var sql =
                @"UPDATE mobile_statistics_events SET name = @Name,
                        date = @Date,  
                        description = @Description";
            dbconnection.Execute(sql, entity);
        }
        finally
        {
            dbconnection.Close();
        }
    }
}
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
    /// <summary>
    /// Подключение базы данных.
    /// </summary>
    private readonly IDbConnection dbconnection;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="dbconnection">Соединение.</param>
    public MobileStatisticsRepository(IDbConnection dbconnection)
    {
        this.dbconnection = dbconnection;
    }

    /// <summary>
    /// Добавление новой сущности.
    /// </summary>
    /// <param name="entity">Новая сущность.</param>
    public void Add(MobileStatisticsItem entity)
    {
        dbconnection.Open();
        try
        {
            entity.Id = Guid.NewGuid();
            var sql =
                @"INSERT INTO mobile_statistics ( id, title, last_statistics, version_client, type)
                VALUES(@Id, @Title, @LastStatistics, @VersionClient, @Type);";
            dbconnection.Execute(sql, entity);
        }
        finally
        {
            dbconnection.Close();
        }
    }

    /// <summary>
    /// Получение всего списка.
    /// </summary>
    /// <returns>Весь список.</returns>
    public async Task<IReadOnlyList<MobileStatisticsItem>> GetAllAsync()
    {
        dbconnection.Open();
        try
        {
            var sql =
                @"SELECT * FROM mobile_statistics";
            var result = await dbconnection.QueryAsync<MobileStatisticsItem>(sql);

            return result.ToList();
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
    /// <returns>объект.</returns>
    public async Task<MobileStatisticsItem> GetByIdAsync(Guid id)
    {
        try
        {
            var sql =
                @"SELECT * FROM mobile_statistics where Id=@Id";
            return await dbconnection.QuerySingleOrDefaultAsync<MobileStatisticsItem>(sql, new { Id = id });
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
    public void Update(MobileStatisticsItem entity)
    {
        dbconnection.Open();
        try
        {
            var sql =
                @"UPDATE mobile_statistics SET title = @Title,
                last_statistics = @LastStatistics,  
                version_client = @VersionClient,
                type = @Type where id=@Id";
            dbconnection.Execute(sql, entity);
        }
        finally
        {
            dbconnection.Close();
        }
    }
}
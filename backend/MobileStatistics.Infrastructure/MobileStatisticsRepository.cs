using Dapper;
using Microsoft.Extensions.Configuration;
using MobileStatisticsApp.Core.Entities;
using MobileStatisticsApp.Repositories;
using Npgsql;

namespace MobileStatisticsApp.Infrastructure;

/// <summary>
/// Репозитарий мобильной статистики.
/// </summary>
public class MobileStatisticsRepository : IMobileStatisticsRepository
{
    private readonly IConfiguration configuration;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="configuration">Конфиг.</param>
    public MobileStatisticsRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    /// <summary>
    /// Добавление новой сущности.
    /// </summary>
    /// <param name="entity">Новая сущность.</param>
    /// <returns>Ок если добавилась.</returns>
    public async Task<bool> AddAsync(MobileStatisticsItem entity)
    {
        entity.Id = Guid.NewGuid();
        entity.LastStatistics = DateTime.Now;
        var sql =
            "INSERT INTO mobile_statistics ( id, title, last_statistics, version_client, type)  VALUES(@Id, @Title, @LastStatistics, @VersionClient, @Type);";
        await using var connection = GetConnection();
        await connection.ExecuteAsync(sql, entity);
        return true;
    }

    /// <summary>
    /// Получение всего списка.
    /// </summary>
    /// <returns>Весь список.</returns>
    public async Task<IReadOnlyList<MobileStatisticsItem>> GetAllAsync()
    {
        var sql =
            "SELECT * FROM mobile_statistics";
        await using var connection = GetConnection();
        var result = await connection.QueryAsync<MobileStatisticsItem>(sql);
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
            "SELECT * FROM mobile_statistics where Id=@Id";
        await using var connection = GetConnection();
        var result = await connection.QuerySingleOrDefaultAsync<MobileStatisticsItem>(sql, new { Id = id });
        return result;
    }

    /// <summary>
    /// Обновление объекта.
    /// </summary>
    /// <param name="entity">Объект для изменения.</param>
    /// <returns>Ок если изменен.</returns>
    public async Task<bool> UpdateAsync(MobileStatisticsItem entity)
    {
        var sql =
            "UPDATE mobile_statistics SET title = @Title," +
            " last_statistics = @LastStatistics, " +
            "version_client = @VersionClient,"+
            " type = @Type";
        await using var connection = GetConnection();
        await connection.ExecuteAsync(sql, entity);
        return true;
    }

    private NpgsqlConnection GetConnection()
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        connection.Open();
        return connection;
    }
}
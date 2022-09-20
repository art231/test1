using Dapper;
using MobileStatisticsApp.Core.Entities;
using MobileStatisticsApp.Repositories;
using MobileStatistics.Application;

namespace MobileStatisticsApp.Infrastructure;

/// <summary>
/// Репозитарий мобильной статистики.
/// </summary>
public class MobileStatisticsRepository : IMobileStatisticsRepository
{
    private readonly IUnitOfWork unitOfWork;
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="unitOfWork">Юнит оф ворк.</param>
    public MobileStatisticsRepository(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    /// <summary>
    /// Добавление новой сущности.
    /// </summary>
    /// <param name="entity">Новая сущность.</param>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    public async Task AddAsync(MobileStatisticsItem entity)
    {
        var sql =
            @"INSERT INTO mobile_statistics ( id, title, last_statistics, version_client, type)
                VALUES(@Id, @Title, @LastStatistics, @VersionClient, @Type);";
        await this.unitOfWork.Connection.ExecuteAsync(sql, entity, this.unitOfWork.Transaction);
    }

    /// <summary>
    /// Получение всего списка.
    /// </summary>
    /// <returns>Весь список.</returns>
    public async Task<IReadOnlyList<MobileStatisticsItem>> GetAllAsync()
    {
        var sql =
            @"SELECT id, title, last_statistics as LastStatistics, version_client as VersionClient, type FROM mobile_statistics";
        var result = await this.unitOfWork.Connection.QueryAsync<MobileStatisticsItem>(sql, this.unitOfWork.Transaction);

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
            @"SELECT id, title, last_statistics as LastStatistics, version_client as VersionClient, type FROM mobile_statistics where Id=@Id";
        return await this.unitOfWork.Connection.QuerySingleOrDefaultAsync<MobileStatisticsItem>(sql, new { Id = id }, this.unitOfWork.Transaction);
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
        await this.unitOfWork.Connection.ExecuteAsync(sql, entity, this.unitOfWork.Transaction);
    }
}
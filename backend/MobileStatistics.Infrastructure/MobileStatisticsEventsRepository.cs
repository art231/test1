using Dapper;
using MobileStatistics.Application;
using MobileStatisticsApp.Application.Repositories;
using MobileStatisticsApp.Core.Entities;

namespace MobileStatisticsApp.Infrastructure;

/// <summary>
/// Репозитарий событий мобильной статистики.
/// </summary>
public class MobileStatisticsEventsRepository : IMobileStatisticsEventsRepository
{
    private readonly IUnitOfWork unitOfWork;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="unitOfWork">Юнит оф ворк.</param>
    public MobileStatisticsEventsRepository(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
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
        await this.unitOfWork.Connection.ExecuteAsync(sql, entities, this.unitOfWork.Transaction);
    }

    /// <summary>
    /// Получение по ключу объекта.
    /// </summary>
    /// <param name="mobileStatisticsId">Уникальный идентификатор.</param>
    /// <returns>Объект событий.</returns>
    public async Task<IEnumerable<MobileStatisticsEvent>> GetByIdAsync(Guid mobileStatisticsId)
    {
        var sql =
            @"SELECT  id, mobile_statistics_id as MobileStatisticsId, date, name, description FROM mobile_statistics_events where mobile_statistics_id = @MobileStatisticsId";
        return await this.unitOfWork.Connection.QueryAsync<MobileStatisticsEvent>(sql,
            new { MobileStatisticsId = mobileStatisticsId }, this.unitOfWork.Transaction);
    }
}
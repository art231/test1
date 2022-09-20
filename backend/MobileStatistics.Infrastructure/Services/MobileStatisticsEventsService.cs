using MobileStatisticsApp.Application.Repositories;
using MobileStatisticsApp.Application.Services;
using MobileStatisticsApp.Core.Entities;
using System.Data;

namespace MobileStatisticsApp.Infrastructure.Services;
/// <summary>
/// Сервис.
/// </summary>
public class MobileStatisticsEventsService: IMobileStatisticsEventsService
{
    private IMobileStatisticsEventsRepository mobileStatisticsEventsRepository;

    private IDbConnection dbConnection;
    /// <summary>
    /// Сервис событий.
    /// </summary>
    /// <param name="mobileStatisticsRepository">Репозиторий статистики.</param>
    /// <param name="dalSession">Новое подключение.</param>
    public MobileStatisticsEventsService(IMobileStatisticsEventsRepository mobileStatisticsRepository,
        IDbConnection dbConnection)
    {
        this.mobileStatisticsEventsRepository = mobileStatisticsRepository;
        this.dbConnection = dbConnection;
    }
    /// <summary>
    /// Добавление нового события.
    /// </summary>
    /// <param name="entities">Новая сущность.</param>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    public async Task CreateEventsAsync(IEnumerable<MobileStatisticsEvent> entities)
    {
        using DalSession dalSession = new DalSession(dbConnection);
        UnitOfWork unitOfWork = dalSession.UnitOfWork;
        unitOfWork.Begin();
        try
        {
            await this.mobileStatisticsEventsRepository.CreateEventsAsync(entities);
            unitOfWork.Commit();
        }
        catch
        {
            unitOfWork.Rollback();
            throw;
        }
    }
    /// <summary>
    /// Получение по ключу объекта.
    /// </summary>
    /// <param name="mobileStatisticsId">Уникальный идентификатор.</param>
    /// <returns>Объект событий.</returns>
    public async Task<IEnumerable<MobileStatisticsEvent>> GetByIdAsync(Guid mobileStatisticsId)
    {
        IEnumerable<MobileStatisticsEvent> result;
        using DalSession dalSession = new DalSession(dbConnection);
        UnitOfWork unitOfWork = dalSession.UnitOfWork;
        unitOfWork.Begin();
        try
        {
            result = await this.mobileStatisticsEventsRepository.GetByIdAsync(mobileStatisticsId);
            unitOfWork.Commit();
        }
        catch
        {
            unitOfWork.Rollback();
            throw;
        }

        return result;
    }
}
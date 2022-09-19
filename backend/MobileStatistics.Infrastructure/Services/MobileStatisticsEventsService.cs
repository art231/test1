using MobileStatisticsApp.Application.Repositories;
using MobileStatisticsApp.Application.Services;
using MobileStatisticsApp.Core.Entities;

namespace MobileStatisticsApp.Infrastructure.Services;
/// <summary>
/// Сервис.
/// </summary>
public class MobileStatisticsEventsService: IMobileStatisticsEventsService
{
    private IMobileStatisticsEventsRepository mobileStatisticsEventsRepository;
    private IDalSession dalSession;
    private UnitOfWork unitOfWork;
    /// <summary>
    /// Сервис событий.
    /// </summary>
    /// <param name="mobileStatisticsRepository">Репозиторий статистики.</param>
    /// <param name="dalSession">Новое подключение.</param>
    public MobileStatisticsEventsService(IMobileStatisticsEventsRepository mobileStatisticsRepository,
        IDalSession dalSession)
    {
        this.mobileStatisticsEventsRepository = mobileStatisticsRepository;
        this.dalSession = dalSession;
        this.unitOfWork = this.dalSession.UnitOfWork;
    }
    /// <summary>
    /// Добавление нового события.
    /// </summary>
    /// <param name="entities">Новая сущность.</param>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    public async Task CreateEventsAsync(IEnumerable<MobileStatisticsEvent> entities)
    {
        await this.mobileStatisticsEventsRepository.CreateEventsAsync(entities);
    }
    /// <summary>
    /// Получение по ключу объекта.
    /// </summary>
    /// <param name="mobileStatisticsId">Уникальный идентификатор.</param>
    /// <returns>Объект событий.</returns>
    public async Task<IEnumerable<MobileStatisticsEvent>> GetByIdAsync(Guid mobileStatisticsId)
    {
        IEnumerable<MobileStatisticsEvent> result;
        this.unitOfWork.Begin();
        try
        {
            result = await this.mobileStatisticsEventsRepository.GetByIdAsync(mobileStatisticsId);

            this.unitOfWork.Commit();
        }
        catch
        {
            this.unitOfWork.Rollback();
            throw;
        }
        return result;
    }
}
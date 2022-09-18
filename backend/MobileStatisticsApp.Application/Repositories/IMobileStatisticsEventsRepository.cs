using MobileStatisticsApp.Core.Entities;

namespace MobileStatisticsApp.Application.Repositories;
/// <summary>
/// Репозитарий событий мобильной статистики.
/// </summary>
public interface IMobileStatisticsEventsRepository
{
    /// <summary>
    /// Создание события.
    /// </summary>
    /// <param name="entities">Сущность события мобильной статистики.</param>
    /// <returns>Выполнение.</returns>
    Task CreateEventsAsync(IEnumerable<MobileStatisticsEvent> entities);
    /// <summary>
    /// Обновление события мобильной статистики.
    /// </summary>
    /// <param name="entity">Модель изменения.</param>
    /// <returns>Выполнение обновления события.</returns>
    Task UpdateEventAsync(MobileStatisticsEvent entity);
    /// <summary>
    /// Получение события мобильной статистики.
    /// </summary>
    /// <param name="mobileStatisticsId">Сущность события мобильной статистики.</param>
    /// <returns>Событие мобильной статистики.</returns>
    Task<IEnumerable<MobileStatisticsEvent>> GetListEventsByIdAsync(Guid mobileStatisticsId);
    /// <summary>
    /// Получение события мобильной статистики..
    /// </summary>
    /// <param name="mobileStatisticsEventId">Ключ события мобильной статистики.</param>
    /// <returns>Событие.</returns>
    Task<MobileStatisticsEvent> GetEventByIdAsync(Guid mobileStatisticsEventId);
    /// <summary>
    /// Удаление события.
    /// </summary>
    /// <param name="mobileStatisticsEventId">Ключ для удаления.</param>
    /// <returns>Удаление мобильной статистики.</returns>
    Task DeleteAsync(Guid mobileStatisticsEventId);
}
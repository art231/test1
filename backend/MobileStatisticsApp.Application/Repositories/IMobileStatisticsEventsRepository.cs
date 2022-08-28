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
    /// <param name="entity">Сущность события мобильной статистики.</param>
    /// <returns>Выполнение.</returns>
    Task CreateEvent(IEnumerable<MobileStatisticsEvent> entity);
    /// <summary>
    /// Получение события мобильной статистики.
    /// </summary>
    /// <param name="eventId">Сущность события мобильной статистики.</param>
    /// <returns>Событие мобильной статистики.</returns>
    Task<IEnumerable<MobileStatisticsEvent>> GetByIdAsync(Guid mobileStatisticsId);
}
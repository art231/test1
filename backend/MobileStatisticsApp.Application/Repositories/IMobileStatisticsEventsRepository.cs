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
    /// Получение события мобильной статистики.
    /// </summary>
    /// <param name="mobileStatisticsId">Сущность события мобильной статистики.</param>
    /// <returns>Событие мобильной статистики.</returns>
    Task<IEnumerable<MobileStatisticsEvent>> GetByIdAsync(Guid mobileStatisticsId);
}
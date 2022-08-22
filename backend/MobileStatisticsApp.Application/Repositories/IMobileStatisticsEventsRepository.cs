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
    void CreateEvent(MobileStatisticsEvents entity);
    /// <summary>
    /// Получение события мобильной статистики.
    /// </summary>
    /// <param name="id">Сущность события мобильной статистики.</param>
    /// <returns>Событие мобильной статистики.</returns>
    Task<IEnumerable<MobileStatisticsEvents>> GetByIdAsync(Guid id);
    /// <summary>
    /// Обновление события мобильной статистики.
    /// </summary>
    /// <param name="entity">Сущность события мобильной статистики.</param>

    void Update(MobileStatisticsEvents entity);
}
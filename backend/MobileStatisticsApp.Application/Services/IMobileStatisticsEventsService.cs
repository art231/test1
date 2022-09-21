using MobileStatisticsApp.Core.Entities;

namespace MobileStatisticsApp.Application.Services;
/// <summary>
/// События.
/// </summary>
public interface IMobileStatisticsEventsService
{
    /// <summary>
    /// Создание событий.
    /// </summary>
    /// <param name="entities">События.</param>
    /// <returns>Ок.</returns>
    Task CreateEventsAsync(IEnumerable<MobileStatisticsEvent> entities);
    /// <summary>
    /// Получение событий.
    /// </summary>
    /// <param name="mobileStatisticsId">Ключ статистики.</param>
    /// <returns>Количество событий.</returns>
    Task<IEnumerable<MobileStatisticsEvent>> GetByIdAsync(Guid mobileStatisticsId);
}
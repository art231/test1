using MobileStatisticsApp.Core.Entities;

namespace MobileStatisticsApp.Application.Services
{
    /// <summary>
    /// Сервис.
    /// </summary>
    public interface IMobileStatisticsService
    {
        /// <summary>
        /// Получение всего списка.
        /// </summary>
        /// <returns>Количество.</returns>
        Task<IReadOnlyList<MobileStatisticsItem>> GetAllAsync();
        /// <summary>
        /// Получение по id.
        /// </summary>
        /// <param name="id">Ключ.</param>
        /// <returns>Элемент.</returns>
        Task<MobileStatisticsItem> GetByIdAsync(Guid id);
        /// <summary>
        /// Добавление узла.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Ок.</returns>
        Task AddAsync(MobileStatisticsItem entity);
        /// <summary>
        /// Обновление.
        /// </summary>
        /// <param name="entity">сущность.</param>
        /// <returns>Ок.</returns>
        Task UpdateAsync(MobileStatisticsItem entity);

    }
}

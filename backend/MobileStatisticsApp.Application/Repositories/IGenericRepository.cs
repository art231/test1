namespace MobileStatisticsApp.Repositories;

/// <summary>
///     Общий репозитарий.
/// </summary>
/// <typeparam name="T">Уникальный тип.</typeparam>
public interface IGenericRepository<T> where T : class
{
    /// <summary>
    ///     Получение по id.
    /// </summary>
    /// <param name="id">Уникальный номер.</param>
    /// <returns>Мобильная статистика.</returns>
    Task<T> GetByIdAsync(Guid id);

    /// <summary>
    ///     Получение всего списка.
    /// </summary>
    /// <returns>Весь список.</returns>
    Task<IReadOnlyList<T>> GetAllAsync();

    /// <summary>
    ///     Добавление новой сущности.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Если ок, то true.</returns>
    Task<bool> AddAsync(T entity);

    /// <summary>
    ///     Обновление сущности.
    /// </summary>
    /// <param name="entity">Сущность для изменения.</param>
    /// <returns>Если ок, то true.</returns>
    Task<bool> UpdateAsync(T entity);
}
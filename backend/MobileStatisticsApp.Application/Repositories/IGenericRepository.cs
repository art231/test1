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
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    Task AddAsync(T entity);

    /// <summary>
    ///     Обновление сущности.
    /// </summary>
    /// <param name="entity">Сущность для изменения.</param>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    Task UpdateAsync(T entity);
}
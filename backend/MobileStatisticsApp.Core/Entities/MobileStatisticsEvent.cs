namespace MobileStatisticsApp.Core.Entities;
/// <summary>
/// Событие мобильной статистики.
/// </summary>
public class MobileStatisticsEvent
{
    /// <summary>
    ///     Уникальный ключ событий.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Уникальный ключ мобильной статистики.
    /// </summary>
    public Guid MobileStatisticsId { get; set; }

    /// <summary>
    ///     Дата последней статистики.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    ///     Название мобильной статистики.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Версия клиента.
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Фабрика создания элемента.
    /// </summary>
    /// <param name="id">Уникальный номер события.</param>
    /// <param name="mobileStatisticsId">Уникальный номер статистики.</param>
    /// <param name="date">Время.</param>
    /// <param name="name">Название.</param>
    /// <param name="description">Описание.</param>
    /// <returns>Новый объект фабрики.</returns>
    public MobileStatisticsEvent CreateNewEvent(Guid id, Guid mobileStatisticsId, DateTime date, string? name, string? description)
    {
        return new MobileStatisticsEvent()
        {
            Id = Guid.NewGuid(),
            MobileStatisticsId = mobileStatisticsId,
            Date = date,
            Name = name,
            Description = description
        };
    }
}
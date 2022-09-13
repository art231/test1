namespace MobileStatisticsApp.Core.Entities;

/// <summary>
///     Событие мобильной статистики.
/// </summary>
public class MobileStatisticsEvent
{
    /// <summary>
    ///     Конструктор событий.
    /// </summary>
    /// <param name="id">Ключ.</param>
    /// <param name="mobileStatisticsId">Ключ статистики.</param>
    /// <param name="date">Время.</param>
    /// <param name="name">Название.</param>
    /// <param name="description">Описание.</param>
    private MobileStatisticsEvent(Guid id, Guid mobileStatisticsId, DateTime date, string? name, string? description)
    {
        Id = id;
        MobileStatisticsId = mobileStatisticsId;
        Date = date;
        Name = name;
        Description = description;
    }

    /// <summary>
    ///     Уникальный ключ событий.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    ///     Уникальный ключ мобильной статистики.
    /// </summary>
    public Guid MobileStatisticsId { get; }

    /// <summary>
    ///     Дата последней статистики.
    /// </summary>
    public DateTime Date { get; }

    /// <summary>
    ///     Название мобильной статистики.
    /// </summary>
    public string? Name { get; }

    /// <summary>
    ///     Версия клиента.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    ///     Фабрика создания элемента.
    /// </summary>
    /// <param name="mobileStatisticsId">Уникальный номер статистики.</param>
    /// <param name="date">Время.</param>
    /// <param name="name">Название.</param>
    /// <param name="description">Описание.</param>
    /// <returns>Новый объект фабрики.</returns>
    public static MobileStatisticsEvent CreateNewEvent(Guid mobileStatisticsId, DateTime date, string? name,
        string? description)
    {
        return new MobileStatisticsEvent(Guid.NewGuid(), mobileStatisticsId, date, name, description);
    }
}
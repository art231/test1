namespace MobileStatisticsApp.Core.Entities;
/// <summary>
/// Событие мобильной статистики.
/// </summary>
public class MobileStatisticsEvents
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
}
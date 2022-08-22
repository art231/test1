namespace MobileStatisticsApp.Api.Dtos;
/// <summary>
/// Модель для отображения мобильной статистики.
/// </summary>
public record MobileStatisticsEventsDto
{
    /// <summary>
    ///     Уникальный ключ событий.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    ///     Дата последней статистики.
    /// </summary>
    public DateTime Date { get; init; }

    /// <summary>
    ///     Название мобильной статистики.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    ///     Версия клиента.
    /// </summary>
    public string? Description { get; init; }
}

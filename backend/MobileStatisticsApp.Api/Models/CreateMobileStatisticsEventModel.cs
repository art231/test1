namespace MobileStatisticsApp.Api.Models;
/// <summary>
/// Модель создания нового события.
/// </summary>
public sealed record CreateMobileStatisticsEventModel
{
    /// <summary>
    ///     Уникальный ключ мобильной статистики.
    /// </summary>
    public Guid MobileStatisticsId { get; init; }

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

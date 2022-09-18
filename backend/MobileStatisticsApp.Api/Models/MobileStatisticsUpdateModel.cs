namespace MobileStatisticsApp.Api.Models;

/// <summary>
/// Модель создания статистики.
/// </summary>
public sealed record MobileStatisticsUpdateModel
{
    /// <summary>
    /// Уникальный ключ.
    /// </summary>
    public Guid Id { get; init; }
    /// <summary>
    ///     Название мобильной статистики.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    ///     Дата последней статистики.
    /// </summary>
    public DateTime LastStatistics { get; init; }

    /// <summary>
    ///     Версия клиента.
    /// </summary>
    public string? VersionClient { get; init; }

    /// <summary>
    ///     Тип статистики.
    /// </summary>
    public string? Type { get; init; }
}

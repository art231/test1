namespace MobileStatisticsApp.Api.Models;

/// <summary>
/// Модель обновления события мобильной статистики.
/// </summary>
public sealed record UpdateMobileStatisticsEventModel
{
    /// <summary>
    /// Уникальный ключ.
    /// </summary>
    public Guid Id { get; init; }
    /// <summary>
    /// Описание мобильной статистики.
    /// </summary>
    public string? Description { get; init; }
}

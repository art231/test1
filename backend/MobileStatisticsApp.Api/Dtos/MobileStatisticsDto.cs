namespace MobileStatisticsApp.Dtos;

/// <summary>
/// Внешняя модель данных.
/// </summary>
public class MobileStatisticsDto
{
    /// <summary>
    /// Уникальный ключ мобильной статистики.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название мобильной статистики.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Дата последней статистики.
    /// </summary>
    public DateTime LastStatistics { get; set; }

    /// <summary>
    /// Версия клиента.
    /// </summary>
    public string? VersionClient { get; set; }

    /// <summary>
    /// Тип статистики.
    /// </summary>
    public string? Type { get; set; }
}

namespace MobileStatisticsApp.Core.Entities;

/// <summary>
///     Модель мобильной статистики.
/// </summary>
public sealed class MobileStatisticsItem 
{
    /// <summary>
    ///     Конструктор мобильной статистики.
    /// </summary>
    /// <param name="id">Ключ.</param>
    /// <param name="title">Название.</param>
    /// <param name="lastStatistics">Время последней статистики.</param>
    /// <param name="versionClient">Версия клиента.</param>
    /// <param name="type">Тип устройства.</param>
    private MobileStatisticsItem(
        Guid id,
        string? title,
        DateTime lastStatistics,
        string? versionClient,
        string? type)
    {
        Id = id;
        Title = title;
        LastStatistics = lastStatistics;
        VersionClient = versionClient;
        Type = type;
    }

    /// <summary>
    ///     Уникальный ключ мобильной статистики.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    ///     Название мобильной статистики.
    /// </summary>
    public string? Title { get; private set; }

    /// <summary>
    ///     Дата последней статистики.
    /// </summary>
    public DateTime LastStatistics { get; private set; }

    /// <summary>
    ///     Версия клиента.
    /// </summary>
    public string? VersionClient { get; private set; }

    /// <summary>
    ///     Тип статистики.
    /// </summary>
    public string? Type { get; private set; }

    /// <summary>
    ///     Конструктор мобильной статистики.
    /// </summary>
    /// <param name="title">Название.</param>
    /// <param name="lastStatistics">Время последнец статистики.</param>
    /// <param name="versionClient">Версия клиента.</param>
    /// <param name="type">Тип устройства.</param>
    /// <returns>Новая статистика.</returns>
    public static MobileStatisticsItem CreateMobileStatisticsItem(
        string? title,
        DateTime lastStatistics,
        string? versionClient,
        string? type)
    {
        return new MobileStatisticsItem(Guid.NewGuid(), title, lastStatistics, versionClient, type);
    }

    /// <summary>
    ///     Фабрика изменения статистики.
    /// </summary>
    /// <param name="id">Ключ.</param>
    /// <param name="title">Название.</param>
    /// <param name="lastStatistics">Последняя статистика.</param>
    /// <param name="versionClient">Версия клиента.</param>
    /// <param name="type">Тип устройства.</param>
    public void UpdateMobileStatisticsItem(Guid id,
        string? title,
        DateTime lastStatistics,
        string? versionClient,
        string? type)
    {
        Id = Id;
        Title = title;
        LastStatistics = lastStatistics;
        VersionClient = versionClient;
        Type = type;
    }
}
namespace MobileStatisticsApp.Api.Dtos;
/// <summary>
/// Модель для отображения списка мобильной статистики.
/// </summary>
public class MobileStatisticsWithEventsDto
{
    /// <summary>
    /// Уникальный ключ мобильной статистики.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// События мобильной статистики.
    /// </summary>
    public IEnumerable<MobileStatisticsEventsDto>? Events { get; set; }
}

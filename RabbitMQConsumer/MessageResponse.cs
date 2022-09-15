namespace RabbitMQConsumer;

public sealed class MessageResponse
{
    /// <summary>
    /// Ключ мобильной статистики.
    /// </summary>
    public Guid NodeId { get; init; }
    /// <summary>
    /// Ключ события.
    /// </summary>
    public Guid EventId { get; init; }
    /// <summary>
    /// Время отправки.
    /// </summary>
    public DateTime Date { get; init; }
    /// <summary>
    /// Название события.
    /// </summary>
    public string? Name { get; init; }
}
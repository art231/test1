namespace MobileStatisticsApp.Infrastructure.Services
{
    /// <summary>
    /// Интерфейс для отправки сообщений.
    /// </summary>
    public interface IRabbitMqService
    {
        /// <summary>
        /// Отправка сообщений.
        /// </summary>
        /// <param name="obj">Любой тип сообщения.</param>
        void SendMessage(object obj);
        /// <summary>
        /// Отправка сообщений.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        void SendMessage(string message);
    }
}

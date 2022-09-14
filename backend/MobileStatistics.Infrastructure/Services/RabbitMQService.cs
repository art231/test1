using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MobileStatisticsApp.Infrastructure.Services
{
    /// <summary>
    /// Инициализация отправки сообщений.
    /// </summary>
    public class RabbitMQService : IRabbitMqService
    {
        private readonly IConfiguration configuration;
        /// <summary>
        /// Конструктор отправки сообщений.
        /// </summary>
        /// <param name="configuration">Настройки подключения.</param>
        public RabbitMQService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        /// <summary>
        /// Отправка сообщений.
        /// </summary>
        /// <param name="obj">Тело сообщения.</param>
        public void SendMessage(object obj)
        {
            var message = JsonSerializer.Serialize(obj);
            SendMessage(message);
        }
        /// <summary>
        /// Отправка сообщения.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = this.configuration["Rabbitmq:Client:Server"]
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: this.configuration["Rabbitmq:Client:Queue"],
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty, 
                routingKey: this.configuration["Rabbitmq:Client:Queue"],
                basicProperties: null,
                body: body);
        }
    }
}

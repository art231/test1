using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using RabbitMQConsumer;
using Newtonsoft.Json;

var factory = new ConnectionFactory() { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare("MyQueue",
    true,
    false,
    false,
    null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    try
    {
        var message = Encoding.UTF8.GetString(body);
        MessageResponse obj = JsonConvert.DeserializeObject<MessageResponse>(message)!;
        if (obj == null)
        {
            throw new Exception("dont have object");
        }
        Console.WriteLine(" [x] Received {0}", message);
        channel.ConfirmSelect();
    }
    catch (Exception e)
    {
        var factoryError = new ConnectionFactory()
        {
            HostName = "localhost"
        };
        using var connectionError = factoryError.CreateConnection();
        using var channelError = connectionError.CreateModel();
        channelError.QueueDeclare("QueueError",
            true,
            false,
            false,
            null);

        var bodyError = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(e));
        var basicPropertiesError = channelError.CreateBasicProperties();
        basicPropertiesError.Persistent = true;
        channelError.BasicPublish(string.Empty,
            "QueueError",
            basicPropertiesError,
            bodyError);
    }
};
channel.BasicConsume("MyQueue",
    false,
    consumer);


Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
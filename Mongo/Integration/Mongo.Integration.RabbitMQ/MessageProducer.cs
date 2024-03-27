using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Mongo.Integration.RabbitMQ
{
    public class MessageProducer : IMessageProducer
    {
        public void PublishMessage(object message, string topic_queue_Name)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: topic_queue_Name,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var jsonMessage = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            channel.BasicPublish(
                exchange: string.Empty,
                routingKey: topic_queue_Name,
                basicProperties: null,
                body: body);
        }
    }
}

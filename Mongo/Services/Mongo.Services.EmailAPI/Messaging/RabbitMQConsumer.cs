using Mongo.Services.EmailAPI.Models.Dto;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Mongo.Services.EmailAPI.Messaging
{
    public class RabbitMqConsumer : IRabbitMqConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly string _emailShoppingCartQueue;        
        private readonly IConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqConsumer(IConfiguration configuration)
        {
            this._configuration = configuration;
            _emailShoppingCartQueue = _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCartQueue");
            _factory = new ConnectionFactory { HostName = _configuration.GetValue<string>("RabbitMQHostName") };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void StartConsumer()
        {
            _channel.QueueDeclare(
                queue: _emailShoppingCartQueue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += ConsumerReceived;

            _channel.BasicConsume(
                    queue: _emailShoppingCartQueue,
                    autoAck: true,
                    consumer: consumer);
        }

        public void StopConsumer()
        {
            _channel.Dispose();
        }

        private async void ConsumerReceived(object? model, BasicDeliverEventArgs eventArgs)
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            CartDto objMessage = JsonConvert.DeserializeObject<CartDto>(message);
            try
            {
                //TODO - try to log email
                _channel.BasicAck(
                    eventArgs.DeliveryTag,
                    multiple: false);
            }
            catch (Exception ex)
            {
                throw;
            }            
        }
    }
}

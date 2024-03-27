namespace Mongo.Services.EmailAPI.Messaging
{
    public interface IRabbitMqConsumer
    {
        void StartConsumer();
        void StopConsumer();
    }
}

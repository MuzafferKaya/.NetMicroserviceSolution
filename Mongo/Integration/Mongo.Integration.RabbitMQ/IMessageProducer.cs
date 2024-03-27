namespace Mongo.Integration.RabbitMQ
{
    public interface IMessageProducer
    {
        void PublishMessage(object message, string topic_queue_Name);
    }
}

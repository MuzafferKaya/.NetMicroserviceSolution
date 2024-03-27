using Mongo.Services.EmailAPI.Messaging;

namespace Mongo.Services.EmailAPI.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        private static IRabbitMqConsumer consumer { get; set; }
        public static IApplicationBuilder UseRabbitMQConsumer(this IApplicationBuilder app)
        {
            consumer = app.ApplicationServices.GetService<IRabbitMqConsumer>();
            var hostApplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostApplicationLife.ApplicationStarted.Register(OnStart);
            hostApplicationLife.ApplicationStopping.Register(OnStop);

            return app;
        }

        private static void OnStart()
        {
            consumer.StartConsumer();
        }

        private static void OnStop()
        {
            consumer.StopConsumer();
        }
    }
}

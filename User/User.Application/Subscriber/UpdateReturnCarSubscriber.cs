using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using User.Application.Interfaces;
using System.Text;
using Newtonsoft.Json;
using User.Core.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace User.Application.Subscriber
{
    public class UpdateReturnCarSubscriber : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        private const string Queue = "returned-car";
        private const string Exchange = "order-service";
        private const string RoutingKey = "order-service";

        public UpdateReturnCarSubscriber(IServiceProvider serviceProvider)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = connectionFactory.CreateConnection("update-car-subscriber");

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(Exchange, "direct", true);
            _channel.QueueDeclare(Queue, true, false, false, null); ;
            _channel.QueueBind(Queue, "order-service", RoutingKey);
            _serviceProvider = serviceProvider;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, eventArgs) =>
            {
                var byteArray = eventArgs.Body.ToArray();

                var contentString = Encoding.UTF8.GetString(byteArray);
                var message = JsonConvert.DeserializeObject<string>(contentString);

                Console.WriteLine($"Cars received, cars: {message}");

                await UpdateCar(message);
                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(Queue, false, consumer);

            return Task.CompletedTask;
        }

        private async Task<bool> UpdateCar(string message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var carService = scope.ServiceProvider.GetService<ICarService>();

                await carService.UpdateAvailabilityCar(message, CarStatusEnum.Available);
            }

            return true;
        }
    }
}

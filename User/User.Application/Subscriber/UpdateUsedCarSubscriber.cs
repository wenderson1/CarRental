using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.Interfaces;
using User.Core.Enums;

namespace User.Application.Subscriber
{
    public class UpdateUsedCarSubscriber : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        private const string Queue = "used-car";
        private const string Exchange = "order-service";
        private const string RoutingKey = "order-service";

        public UpdateUsedCarSubscriber(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = connectionFactory.CreateConnection("update-cars-subscriber");

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(Exchange, "direct", true);
            _channel.QueueDeclare(Queue, true, false, false, null); ;
            _channel.QueueBind(Queue, "order-service", RoutingKey);
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

                await carService.UpdateAvailabilityCar(message, CarStatusEnum.Busy);

            }
            return true;
        }
    }
}

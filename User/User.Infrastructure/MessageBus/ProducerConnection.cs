using RabbitMQ.Client;

namespace User.Infrastructure.MessageBus
{
    public class ProducerConnection
    {
        public IConnection Connection { get; set; }

        public ProducerConnection(IConnection connection)
        {
            Connection = connection;
        }
    }
}

using System.Text;
using System.Text.Json;
using platformService.Dtos;
using RabbitMQ.Client;

namespace platformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                  Console.WriteLine("----> Rabbit MQ service connected successfully");
            }

            catch (Exception ex)
            {
                Console.WriteLine("----> Could not connect to message bus: ", ex.Message);
            }
        }

        public void PublishNewPlatform(PlatformPublishedDto platformPublishDto)
        {
            var message = JsonSerializer.Serialize(platformPublishDto);

            if (_connection.IsOpen)
            {
                Console.WriteLine("----> Rabbit MQ conenction open sending msg: ");
                sendMessage(message);
            }

        }
        private void sendMessage(string mesage)
        {
            var body = Encoding.UTF8.GetBytes(mesage);

            _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties:null, body:body);

            Console.WriteLine("----> We have sent a message here:", mesage);
        }
        private void RabbitMQ_ConnectionShutdown(Object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("----> Rabbit MQ service shut down  successfully");
        }
    }
}
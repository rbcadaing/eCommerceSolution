using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace BusinessLogicLayer.RabbitMQ;

public class RabbitMQProductNameUpdateConsumer : IDisposable, IRabbitMQProductNameUpdateConsumer
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<RabbitMQProductNameUpdateConsumer> _logger;
    private readonly IChannel _channel;
    private readonly IConnection _connection;

    public RabbitMQProductNameUpdateConsumer(IConfiguration configuration, ILogger<RabbitMQProductNameUpdateConsumer> logger)
    {
        _configuration = configuration;

        Console.WriteLine($"RabbitMQ_HostName: {_configuration["RabbitMQ:HostName"]!}");
        Console.WriteLine($"RabbitMQ_UserName: {_configuration["RabbitMQ:UserName"]!}");
        Console.WriteLine($"RabbitMQ_Password: {_configuration["RabbitMQ:Password"]!}");
        Console.WriteLine($"RabbitMQ_Port: {_configuration["RabbitMQ:Port"]!}");

        _logger = logger;
        string hostName = _configuration["RabbitMQ:HostName"]!;
        string userName = _configuration["RabbitMQ:UserName"]!;
        string password = _configuration["RabbitMQ:Password"]!;
        string port = _configuration["RabbitMQ:Port"]!;

        ConnectionFactory connectionFactory = new ConnectionFactory()
        {
            HostName = hostName,
            UserName = userName,
            Password = password,
            Port = Convert.ToInt32(port)
        };
        _connection = connectionFactory.CreateConnectionAsync().Result;

        _channel = _connection.CreateChannelAsync().Result;
    }


    public void Consume()
    {
        string routingKey = "product.update.name";
        string queueName = "orders.product.update.name.queue";

        //Create exchange
        string exchangeName = _configuration["RabbitMQ_Product_Exchange"]!;
        _channel.ExchangeDeclareAsync(exchange: exchangeName, type: ExchangeType.Direct, durable: true).GetAwaiter();

        //Create message queue
        _channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null).GetAwaiter(); //x-message-ttl | x-max-length | x-expired 

        //Bind the message to exchange
        _channel.QueueBindAsync(queue: queueName, exchange: exchangeName, routingKey: routingKey).GetAwaiter();

        AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync +=  async (sender, args) =>
        {
            await Task.Delay(0); // Simulate some processing delay
            byte[] body = args.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);

            if (message != null)
            {
                ProductNameUpdateMessage? productNameUpdateMessage = JsonSerializer.Deserialize<ProductNameUpdateMessage>(message);

                _logger.LogInformation($"Product name updated: {productNameUpdateMessage!.ProductID}, New name: {productNameUpdateMessage.ProductName}");
            }
        };

        _channel.BasicConsumeAsync(queue: queueName, consumer: consumer, autoAck: true);
    }

    public void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();
    }
}


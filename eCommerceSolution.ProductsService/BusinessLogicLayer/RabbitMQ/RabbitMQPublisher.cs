using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace eCommerce.BusinessLogicLayer.RabbitMQ;

public class RabbitMQPublisher : IRabbitMQPublisher, IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly IChannel _channel;
    private readonly IConnection _connection;

    public RabbitMQPublisher(IConfiguration configuration)
    {
        _configuration = configuration;

        Console.WriteLine($"RabbitMQ_HostName: {_configuration["RabbitMQ:HostName"]!}");
        Console.WriteLine($"RabbitMQ_UserName: {_configuration["RabbitMQ:UserName"]!}");
        Console.WriteLine($"RabbitMQ_Password: {_configuration["RabbitMQ:Password"]!}");
        Console.WriteLine($"RabbitMQ_Port: {_configuration["RabbitMQ:Port"]!}");

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
    public void Publish<T>(T message, string routingKey)
    {
        string messageJson = JsonSerializer.Serialize(message);
        byte[] messageBodyInBytes = Encoding.UTF8.GetBytes(messageJson);

        // Create exchange
        string exchangeName = _configuration["RabbitMQ:ProductExchange"]!;
        _channel.ExchangeDeclareAsync(exchange: exchangeName, type: ExchangeType.Direct, durable: true).Wait();

        // Publish message
        var basicProperties = new BasicProperties(); // Assuming BasicProperties is a valid type
        _channel.BasicPublishAsync(
            exchange: exchangeName,
            routingKey: routingKey,
            mandatory: false,
            basicProperties: basicProperties,
            body: messageBodyInBytes
        ).GetAwaiter();
    }

    //dispose will be called after the service lifetime ends.
    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}

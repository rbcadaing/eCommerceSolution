namespace eCommerce.BusinessLogicLayer.RabbitMQ;

public interface IRabbitMQPublisher
{
    void Publish<T>(T message, string routingKey);
}

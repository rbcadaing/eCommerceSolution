﻿using Microsoft.Extensions.Hosting;

namespace BusinessLogicLayer.RabbitMQ;

public class RabbitMQProductNameUpdateHostedService : IHostedService
{
    private readonly IRabbitMQProductNameUpdateConsumer _productNameUpdateConsumer;

    public RabbitMQProductNameUpdateHostedService(IRabbitMQProductNameUpdateConsumer productNameUpdateConsumer)
    {
        _productNameUpdateConsumer = productNameUpdateConsumer;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _productNameUpdateConsumer.Consume();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _productNameUpdateConsumer.Dispose();
        return Task.CompletedTask;
    }
}

﻿namespace eCommerce.BusinessLogicLayer.RabbitMQ;

public record ProductNameUpdateMessage(
    Guid ProductID,
    string ProductName
);


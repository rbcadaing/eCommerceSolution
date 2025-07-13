namespace BusinessLogicLayer.DTO;

public record ProductDTO(Guid ProductID, string? ProductName, int? Category, double UnitPrice, int QuantityInStock);

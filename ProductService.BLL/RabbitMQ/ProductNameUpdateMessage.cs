namespace ProductService.BLL.RabbitMQ;

public record ProductNameUpdateMessage(Guid ProductID, string? NewName);

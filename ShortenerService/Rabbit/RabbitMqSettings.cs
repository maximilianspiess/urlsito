namespace ShortenerService.Rabbit;

public class RabbitMqSettings
{
    public required string HostName { get; init; }
    public required string QueueName { get; init; }
}
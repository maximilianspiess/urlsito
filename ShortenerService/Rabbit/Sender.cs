using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using ShortenerService.Models;

namespace ShortenerService.Rabbit;

public class Sender: ISender
{
    private readonly RabbitMqSettings _settings;
    
    public Sender(RabbitMqSettings settings)
    {
        _settings = settings;
    }

    public async void Send(NewShortUrlMessage message)
    {
        var factory = new ConnectionFactory { HostName = _settings.HostName };
        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: _settings.QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = JsonSerializer.Serialize(message);
        var bodyBytes = Encoding.UTF8.GetBytes(body);

        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: _settings.QueueName,
            body: bodyBytes
        );

    }
}
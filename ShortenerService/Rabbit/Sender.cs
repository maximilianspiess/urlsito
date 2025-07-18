using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using ShortenerService.Models;

namespace ShortenerService.Rabbit;

public class Sender: ISender
{
    private readonly RabbitMqSettings _settings;
    
    public Sender(IOptions<RabbitMqSettings> settings)
    {
        _settings = settings.Value;
        Init();
    }

    private async void Init()
    {
        var factory = new ConnectionFactory { HostName = _settings.HostName };
        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(
            exchange: "urlsito",
            type: "direct",
            durable: true,
            autoDelete: false,
            arguments: null);

        await channel.QueueDeclareAsync(
            queue: _settings.QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        await channel.QueueBindAsync(
            queue: _settings.QueueName,
            exchange: "urlsito",
            routingKey: string.Empty);   
    }

    public async void Send(NewShortUrlMessage message)
    {
        var factory = new ConnectionFactory { HostName = _settings.HostName };
        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();

        var body = JsonSerializer.Serialize(message);
        var bodyBytes = Encoding.UTF8.GetBytes(body);

        await channel.BasicPublishAsync(
            exchange: "urlsito",
            routingKey: string.Empty,
            body: bodyBytes
        );

        await connection.CloseAsync();
    }
}
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ShortenerService.Models;

namespace ResolverService.Rabbit;

public class Receiver : BackgroundService
{
    private readonly MessageHandler _handler;

    public Receiver(MessageHandler handler)
    {
        _handler = handler;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory{ HostName = "localhost"};
        await using var connection = await factory.CreateConnectionAsync(cancellationToken: stoppingToken);
        await using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await channel.QueueDeclareAsync("NewShortLinkQueue", cancellationToken: stoppingToken);
        
        await channel.QueueBindAsync(
            queue: "NewShortLinkQueue",
            exchange: string.Empty,
            routingKey: string.Empty,
            cancellationToken: stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(channel);
        
        consumer.ReceivedAsync += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = JsonSerializer.Deserialize<NewShortUrlMessage>(body);
            _handler.HandleMessage(message);
            return Task.CompletedTask;
        };

        await channel.BasicConsumeAsync("NewShortLinkQueue", autoAck: true, consumer, cancellationToken: stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(100, stoppingToken);
        }
    }
}
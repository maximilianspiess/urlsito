using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ShortenerService.Models;

namespace ResolverService.Rabbit;

public class Receiver : BackgroundService
{
    private readonly MessageHandler _handler;
    private IConnection _connection = null;
    private IChannel _channel = null;

    public Receiver(MessageHandler handler)
    {
        _handler = handler;
    }

    private async void Run(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };
        _connection = await factory.CreateConnectionAsync(cancellationToken: cancellationToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

        await _channel.QueueDeclareAsync(
            queue: "NewShortLinkQueue",
            durable: false,
            exclusive: false,
            autoDelete: false,
            cancellationToken: cancellationToken);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        
        consumer.ReceivedAsync += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = JsonSerializer.Deserialize<NewShortUrlMessage>(body);
            _handler.HandleMessage(message);
            return Task.CompletedTask;
        };

        await _channel.BasicConsumeAsync("NewShortLinkQueue", autoAck: true, consumer, cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        Run(cancellationToken);
        return Task.CompletedTask;

    }
}
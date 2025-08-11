namespace StatisticsService.Clickhouse;

public class ClickhouseSettings
{
    public required string Host { get; init; }
    public required string Protocol { get; init; }
    public required string Port { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
}
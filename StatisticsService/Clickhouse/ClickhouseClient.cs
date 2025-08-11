using ClickHouse.Client.ADO;
using ClickHouse.Client.ADO.Parameters;
using ClickHouse.Client.Utility;
using Microsoft.Extensions.Options;
using StatisticsService.Model;
using StatisticsService.Models;

namespace StatisticsService.Clickhouse;

public class ClickhouseClient: IClickhouseClient
{
    private ClickHouseConnection _connection;
    private ClickhouseSettings _settings;

    public ClickhouseClient(IOptions<ClickhouseSettings> settings)
    {
        _settings = settings.Value;
        _connection = new ClickHouseConnection(
            $"Host=<{_settings.Host}>;Protocol={_settings.Protocol};Port={_settings.Port}" +
            $";Username={_settings.Username};Password={_settings.Password}");

        using var command = _connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS url_mapping(
                shortUrl String,
                longUrl String
            ) ENGINE = MergeTree()
            ORDER BY shortUrl";

        command.ExecuteNonQuery();
    }
    
    public ClickhouseEntry GetEntryByShortUrl(string shortUrl)
    {
        throw new NotImplementedException();
    }

    public ClickhouseEntry[] GetAllEntries()
    {
        throw new NotImplementedException();
    }

    public async void AddEntry(ClickhouseEntry entry)
    {
        await using var insertCommand = _connection.CreateCommand();
        insertCommand.CommandText = "INSERT INTO url_mapping (shortUrl, longUrl) VALUES @bulk";
        
        insertCommand.Parameters.Add(new ClickHouseDbParameter
        {
            ParameterName = "bulk",
            Value = new[]
            {
                new Object[] {entry.ShortUrl, entry.LongUrl}
            }
        });

        await insertCommand.ExecuteNonQueryAsync();
    }
}
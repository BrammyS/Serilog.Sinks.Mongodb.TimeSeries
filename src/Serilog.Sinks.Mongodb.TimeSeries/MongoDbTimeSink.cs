using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Serilog.Events;
using Serilog.Sinks.Mongodb.TimeSeries.Configurations;
using Serilog.Sinks.Mongodb.TimeSeries.Extensions;
using Serilog.Sinks.Mongodb.TimeSeries.Models;
using Serilog.Sinks.PeriodicBatching;

namespace Serilog.Sinks.Mongodb.TimeSeries;

/// <summary>
///     Saves batches of <see cref="LogEvent" />s to the mongodb database.
/// </summary>
internal class MongoDbTimeSink : IBatchedLogEventSink
{
    private readonly IMongoCollection<LogDocument> _collection;
    private readonly IFormatProvider? _formatProvider;

    /// <summary>
    ///     Initializes a new <see cref="MongoDbTimeSink" />.
    /// </summary>
    /// <param name="config">The <see cref="MongoDbTimeSeriesSinkConfig" /> that will be used to configure the Mongodb sink.</param>
    /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
    internal MongoDbTimeSink(MongoDbTimeSeriesSinkConfig config, IFormatProvider? formatProvider = null)
    {
        _formatProvider = formatProvider;
        if (!config.Database.CollectionExists(config.CollectionName)) config.Database.CreateCollection(config.CollectionName, config.CreateCollectionOptions);

        LogCollectionConfig.ConfigureLogDocumentCollection();

        _collection = config.Database.GetCollection<LogDocument>(config.CollectionName);
    }

    /// <inheritdoc />
    public async Task EmitBatchAsync(IEnumerable<LogEvent> batch)
    {
        var logs = batch.ToDocuments(_formatProvider);
        await _collection.InsertManyAsync(logs).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public Task OnEmptyBatchAsync()
    {
        return Task.CompletedTask;
    }
}
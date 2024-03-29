﻿using System;
using MongoDB.Driver;

namespace Serilog.Sinks.Mongodb.TimeSeries.Configurations;

/// <summary>
///     Contains the configurations for the mongodb serilog sink.
/// </summary>
public record MongoDbTimeSeriesSinkConfig
{
    private const string TimeSeriesPropertyName = "timestamp";

    /// <summary>
    ///     Initializes a new <see cref="MongoDbTimeSeriesSinkConfig" />.
    /// </summary>
    /// <param name="database">The <see cref="IMongoDatabase" /> of where the `logs` collection will be stored.</param>
    public MongoDbTimeSeriesSinkConfig(IMongoDatabase database)
    {
        Database = database;
    }

    /// <summary>
    ///     The collection name of where the logs will be stored. The default is "logs".
    /// </summary>
    public string CollectionName { get; init; } = "logs";

    /// <summary>
    ///     The maximum number of log events to include in a single batch. The default is 1000.
    /// </summary>
    public int BatchSizeLimit { get; init; } = 1000;

    /// <summary>
    ///     Maximum number of events to hold in the sink's internal queue, or null for an unbounded queue. The default is
    ///     10000.
    /// </summary>
    public int QueueLimit { get; init; } = 10000;

    /// <summary>
    ///     Eagerly emit a batch containing the first received event, regardless of the target batch size or batching time.
    ///     This helps with perceived "liveness" when running/debugging applications interactively. The default is true.
    /// </summary>
    public bool EagerlyEmitFirstEvent { get; init; } = true;

    /// <summary>
    ///     The time to wait between checking for event batches. The default is 5 seconds.
    /// </summary>
    public TimeSpan SyncingPeriod { get; init; } = TimeSpan.FromSeconds(5);

    /// <summary>
    ///     The coarse granularity of time-series data. The default is Seconds.
    /// </summary>
    public TimeSeriesGranularity TimeSeriesGranularity { get; init; } = TimeSeriesGranularity.Seconds;

    /// <summary>
    ///     Gets or sets a timespan indicating how long documents in a time series collection should be retained.
    /// </summary>
    public TimeSpan? LogsExpireAfter { get; init; }

    /// <summary>
    ///     The <see cref="MongoDB.Driver.CreateCollectionOptions" /> that will be used to create a new collection
    ///     when no collection with the name of <see cref="CollectionName" /> exists.
    /// </summary>
    internal CreateCollectionOptions CreateCollectionOptions => DefaultCreateCollectionOptions();

    /// <summary>
    ///     The <see cref="IMongoDatabase" /> of where the collection will be stored..
    /// </summary>
    internal IMongoDatabase Database { get; init; }

    /// <summary>
    ///     Get a default implementation of <see cref="MongoDB.Driver.CreateCollectionOptions" />.
    /// </summary>
    /// <returns>
    ///     A default implementation of <see cref="MongoDB.Driver.CreateCollectionOptions" />.
    /// </returns>
    private CreateCollectionOptions DefaultCreateCollectionOptions()
    {
        var defaultTimeSeriesOptions = new TimeSeriesOptions(TimeSeriesPropertyName, null, TimeSeriesGranularity);

        return new CreateCollectionOptions
        {
            TimeSeriesOptions = defaultTimeSeriesOptions,
            ExpireAfter = LogsExpireAfter
        };
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Serilog.Events;
using Serilog.Sinks.Mongodb.TimeSeries.Configurations;
using Serilog.Sinks.Mongodb.TimeSeries.Extensions;
using Serilog.Sinks.Mongodb.TimeSeries.Models;
using Serilog.Sinks.PeriodicBatching;

namespace Serilog.Sinks.Mongodb.TimeSeries
{
    /// <summary>
    ///     Saves batches of <see cref="LogEvent"/>s to the mongodb database.
    /// </summary>
    internal class MongoDbTimeSink : IBatchedLogEventSink
    {
        private readonly MongoDbTimeSeriesSinkConfig _config;
        private readonly IMongoCollection<LogDocument> _collection;
        
        /// <summary>
        ///     Initializes a new <see cref="MongoDbTimeSink" />.
        /// </summary>
        /// <param name="config">The <see cref="MongoDbTimeSeriesSinkConfig"/> that will be used to configure the Mongodb sink.</param>
        internal MongoDbTimeSink(MongoDbTimeSeriesSinkConfig config)
        {
            _config = config;

            if (!CollectionExists())
            {
                _config.Database.CreateCollection(_config.CollectionName, _config.Options);
            }

            LogCollectionConfig.ConfigureCollection();
            
            _collection = _config.Database.GetCollection<LogDocument>(_config.CollectionName);
        }
        
        /// <inheritdoc />
        public async Task EmitBatchAsync(IEnumerable<LogEvent> batch)
        {
            var logs = new List<LogDocument>();

            foreach (var logEvent in batch.ToList())
            {
                logs.Add(new LogDocument
                {
                    Exception = logEvent.Exception,
                    Level = logEvent.Level,
                    Properties = logEvent.Properties.ToDictionary(x => x.Key.ToSaveAbleString(), x => x.Value.ToString()),
                    Timestamp = logEvent.Timestamp.UtcDateTime,
                    Message = logEvent.MessageTemplate.ToString()
                });
            }

            if (!logs.Any()) return;

            await _collection.InsertManyAsync(logs).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task OnEmptyBatchAsync() => Task.CompletedTask;

        /// <summary>
        ///     Checks whether or not a collection exists.
        /// </summary>
        /// <returns>
        ///     Whether or not a collection exists..
        /// </returns>
        private bool CollectionExists()
        {
            var filter = new BsonDocument("name", _config.CollectionName);
            var collectionCursor = _config.Database.ListCollections(new ListCollectionsOptions {Filter = filter});
            return collectionCursor.Any();
        }
    }
}
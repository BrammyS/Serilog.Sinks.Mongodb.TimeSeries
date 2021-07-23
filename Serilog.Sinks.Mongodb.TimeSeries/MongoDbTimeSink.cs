using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using Serilog.Events;
using Serilog.Sinks.Mongodb.TimeSeries.Configurations;
using Serilog.Sinks.Mongodb.TimeSeries.Extensions;
using Serilog.Sinks.PeriodicBatching;

namespace Serilog.Sinks.Mongodb.TimeSeries
{
    /// <summary>
    ///     Saves batches of <see cref="LogEvent"/>s to the mongodb database.
    /// </summary>
    internal class MongoDbTimeSink : IBatchedLogEventSink
    {
        private readonly MongoDbTimeSeriesSinkConfig _config;
        private readonly IMongoCollection<Models.Log> _collection;
        
        /// <summary>
        ///     Initializes a new <see cref="MongoDbTimeSink" />.
        /// </summary>
        /// <param name="config"></param>
        internal MongoDbTimeSink(MongoDbTimeSeriesSinkConfig config)
        {
            _config = config;

            if (!CollectionExists())
            {
                _config.Database.CreateCollection(_config.CollectionName, _config.Options);
            }
            
            BsonClassMap.RegisterClassMap<Models.Log>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(c => c.ObjectId)
                  .SetIdGenerator(BsonObjectIdGenerator.Instance)
                  .SetIsRequired(true);
                cm.MapField(x => x.Timestamp).SetElementName("timestamp").SetIsRequired(true);
                cm.MapField(x => x.Properties).SetElementName("properties").SetIsRequired(true);
                cm.MapField(x => x.MessageTemplate).SetElementName("messageTemplate").SetIsRequired(true);
                cm.MapField(x => x.Exception).SetElementName("exception").SetIsRequired(false);
            });
            
            _collection = _config.Database.GetCollection<Models.Log>(_config.CollectionName);
        }
        
        /// <inheritdoc />
        public async Task EmitBatchAsync(IEnumerable<LogEvent> batch)
        {
            try
            {
                var logs = new List<Models.Log>();

                foreach (var logEvent in batch.ToList())
                {
                    logs.Add(new Models.Log
                    {
                        Exception = logEvent.Exception,
                        Level = logEvent.Level,
                        Properties = logEvent.Properties.ToDictionary(x => x.Key.ToSaveAbleString(), x => x.Value.ToString()),
                        Timestamp = logEvent.Timestamp.UtcDateTime,
                        MessageTemplate = logEvent.MessageTemplate
                    });
                }

                if (!logs.Any()) return;


                await _collection.InsertManyAsync(logs).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <inheritdoc />
        public Task OnEmptyBatchAsync() => Task.CompletedTask;

        private bool CollectionExists()
        {
            var filter = new BsonDocument("name", _config.CollectionName);
            var collectionCursor = _config.Database.ListCollections(new ListCollectionsOptions {Filter = filter});
            return collectionCursor.Any();
        }
    }
}
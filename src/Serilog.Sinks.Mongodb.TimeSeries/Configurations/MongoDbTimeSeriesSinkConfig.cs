using MongoDB.Driver;

namespace Serilog.Sinks.Mongodb.TimeSeries.Configurations
{
    /// <summary>
    ///     Contains the configurations for the mongodb serilog sink.
    /// </summary>
    public record MongoDbTimeSeriesSinkConfig
    {
        /// <summary>
        ///     Initializes a new <see cref="MongoDbTimeSeriesSinkConfig"/>.
        /// </summary>
        /// <param name="collectionName">The collection name of where the logs will be stored.</param>
        /// <param name="database">The <see cref="IMongoDatabase"/> of where the collection will be stored.</param>
        public MongoDbTimeSeriesSinkConfig(string collectionName, IMongoDatabase database)
        {
            CollectionName = collectionName;
            Database = database;
            
            // Todo: Create a time specific create collections config.
            Options = DefaultCreateCollectionOptions();
        }

        /// <summary>
        ///     Initializes a new <see cref="MongoDbTimeSeriesSinkConfig"/>.
        /// </summary>
        /// <param name="database">The <see cref="IMongoDatabase"/> of where the `logs` collection will be stored.</param>
        public MongoDbTimeSeriesSinkConfig(IMongoDatabase database)
        {
            CollectionName = "logs";
            Database = database;
            Options = DefaultCreateCollectionOptions();
        }
        
        /// <summary>
        ///     The collection name of where the logs will be stored.
        /// </summary>
        public string CollectionName { get; init; }
        
        /// <summary>
        ///     The <see cref="CreateCollectionOptions"/> that will be used to create a new collection
        ///     when no collection with the name of <see cref="CollectionName"/> exists.
        /// </summary>
        internal CreateCollectionOptions Options { get; init; } 
        
        /// <summary>
        ///     The <see cref="IMongoDatabase"/> of where the collection will be stored..
        /// </summary>
        public IMongoDatabase Database { get; init; }
        
        /// <summary>
        ///     Get a default implementation of <see cref="CreateCollectionOptions"/>. 
        /// </summary>
        /// <returns>
        ///     A default implementation of <see cref="CreateCollectionOptions"/>.
        /// </returns>
        private static CreateCollectionOptions DefaultCreateCollectionOptions()
        {
            var defaultTimeSeriesOptions = new TimeSeriesOptions("timestamp", null, new Optional<TimeSeriesGranularity?>(TimeSeriesGranularity.Seconds));
            
            return new CreateCollectionOptions
            {
                TimeSeriesOptions = defaultTimeSeriesOptions,
            };
        }
    }
}
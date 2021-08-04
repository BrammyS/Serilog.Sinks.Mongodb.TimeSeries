using MongoDB.Bson;
using MongoDB.Driver;

namespace Serilog.Sinks.Mongodb.TimeSeries.Extensions
{
    /// <summary>
    ///     Contains all extensions methods for <see cref="IMongoDatabase" />.
    /// </summary>
    internal static class MongoDatabaseExtensions
    {
        /// <summary>
        ///     Checks whether or not a collection exists.
        /// </summary>
        /// <param name="database">The <see cref="IMongoDatabase" />.</param>
        /// <param name="collectionName">The collection name that will be checked whether or not it exists.</param>
        /// <returns>
        ///     Whether or not a collection exists..
        /// </returns>
        internal static bool CollectionExists(this IMongoDatabase database, string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var collectionCursor = database.ListCollections(new ListCollectionsOptions { Filter = filter });
            return collectionCursor.Any();
        }
    }
}
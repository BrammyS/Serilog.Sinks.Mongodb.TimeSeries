using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using Serilog.Sinks.Mongodb.TimeSeries.Models;

namespace Serilog.Sinks.Mongodb.TimeSeries.Configurations
{
    internal static class LogCollectionConfig
    {
        /// <summary>
        ///     Configures the mapping of the <see cref="LogDocument" /> object so it can be stored in a collection.
        /// </summary>
        internal static void ConfigureCollection()
        {
            BsonClassMap.RegisterClassMap<LogDocument>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(c => c.ObjectId)
                  .SetIdGenerator(BsonObjectIdGenerator.Instance)
                  .SetIsRequired(true);
                cm.MapField(x => x.Timestamp).SetElementName("timestamp").SetIsRequired(true);
                cm.MapField(x => x.Message).SetElementName("message").SetIsRequired(true);
                cm.MapField(x => x.Properties).SetElementName("properties").SetIsRequired(false);
                cm.MapField(x => x.Exception).SetElementName("exception").SetIsRequired(false);
                cm.MapField(x => x.Severity).SetElementName("severity").SetIsRequired(false);
            });
        }
    }
}
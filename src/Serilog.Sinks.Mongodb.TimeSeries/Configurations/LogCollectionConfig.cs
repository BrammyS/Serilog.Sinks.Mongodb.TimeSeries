using MongoDB.Bson.Serialization;
using Serilog.Sinks.Mongodb.TimeSeries.Models;

namespace Serilog.Sinks.Mongodb.TimeSeries.Configurations
{
    /// <summary>
    ///     Holds the mongodb collection configuration for the <see cref="LogDocument" /> collection.
    /// </summary>
    internal static class LogCollectionConfig
    {
        /// <summary>
        ///     Configures the mapping of the <see cref="LogDocument" /> object so it can be stored in a collection.
        /// </summary>
        internal static void ConfigureLogDocumentCollection()
        {
            BsonClassMap.RegisterClassMap<LogDocument>(cm =>
            {
                cm.AutoMap();
                cm.MapField(x => x.Timestamp).SetElementName("timestamp").SetIsRequired(true);
                cm.MapField(x => x.Message).SetElementName("message").SetIsRequired(true);
                cm.MapField(x => x.Properties).SetElementName("properties").SetIsRequired(false);
                cm.MapField(x => x.Severity).SetElementName("severity").SetIsRequired(false);
                cm.MapField(x => x.Exception).SetElementName("exception").SetIsRequired(false).SetIgnoreIfNull(true);
                cm.MapField(x => x.StackTrace).SetElementName("stackTrace").SetIsRequired(false).SetIgnoreIfNull(true);
            });
        }
    }
}
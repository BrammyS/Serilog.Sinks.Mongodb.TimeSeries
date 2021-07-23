using System;
using System.Collections.Generic;
using MongoDB.Bson;
using Serilog.Events;

namespace Serilog.Sinks.Mongodb.TimeSeries.Models
{
    internal class Log
    {
        /// <summary>
        ///     The object id of the document.
        /// </summary>
        public BsonObjectId ObjectId { get; init; } = null!;

        /// <summary>
        ///     Properties associated with the event, including those presented in <see cref="Serilog.Events.MessageTemplate" />.
        /// </summary>
        public Dictionary<string, string> Properties { get; init; } = null!;

        /// <summary>
        ///     The time at which the event occurred.
        /// </summary>
        public DateTime Timestamp { get; init; }

        /// <summary>
        ///     The level of the event.
        /// </summary>
        public LogEventLevel Level { get; init;}

        /// <summary>
        ///     The message template describing the event.
        /// </summary>
        public MessageTemplate MessageTemplate { get; init;} = null!;

        /// <summary>
        ///     An exception associated with the event, or null.
        /// </summary>
        public Exception? Exception { get; init;}
    }
}
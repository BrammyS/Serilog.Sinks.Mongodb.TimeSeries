using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace Serilog.Sinks.Mongodb.TimeSeries.Models
{
    /// <summary>
    ///     The document that will be stored in mongodb.
    /// </summary>
    public class LogDocument
    {
        /// <summary>
        ///     Properties associated with the event, including those presented in <see cref="Serilog.Events.MessageTemplate" />.
        /// </summary>
        public Dictionary<string, BsonValue> Properties { get; init; } = null!;

        /// <summary>
        ///     The time at which the event occurred.
        /// </summary>
        public DateTime Timestamp { get; init; }

        /// <summary>
        ///     The log level severity.
        /// </summary>
        public string Severity { get; init; } = null!;

        /// <summary>
        ///     The message template describing the event.
        /// </summary>
        public string Message { get; init; } = null!;

        /// <summary>
        ///     An exception associated with the event, or null.
        /// </summary>
        public Exception? Exception { get; init; }

        /// <summary>
        ///     The stack trace of the error, or null.
        /// </summary>
        public string? StackTrace { get; init; }
    }
}
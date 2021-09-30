using System;
using System.Collections.Generic;
using MongoDB.Bson;
using Serilog.Events;
using Serilog.Sinks.Mongodb.TimeSeries.Models;

namespace Serilog.Sinks.Mongodb.TimeSeries.Extensions
{
    /// <summary>
    ///     Contains all extensions methods for <see cref="LogEvent" />.
    /// </summary>
    internal static class LogEventExtensions
    {
        /// <summary>
        ///     Converts a <see cref="IEnumerable{T}" /> of <see cref="LogEvent" />s to <see cref="IEnumerable{T}" /> of
        ///     <see cref="LogDocument" />s.
        /// </summary>
        /// <param name="logEvents">The log events that will be converted.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <returns>
        ///     The converted <see cref="IEnumerable{T}" /> of <see cref="LogDocument" />s.
        /// </returns>
        internal static IEnumerable<LogDocument> ToDocuments(this IEnumerable<LogEvent> logEvents, IFormatProvider? formatProvider = null)
        {
            var logs = new List<LogDocument>();

            foreach (var logEvent in logEvents)
            {
                var message = logEvent.RenderMessage(formatProvider);
                var properties = new Dictionary<string, BsonValue>();

                foreach (var (key, value) in logEvent.Properties)
                {
                    properties.Add(key.ToSaveAbleString(), value.ToBsonValue(null, formatProvider));
                }

                logs.Add(new LogDocument
                {
                    Exception = logEvent.Exception,
                    StackTrace = logEvent.Exception.StackTrace,
                    Severity = logEvent.Level.ToSeverityString(),
                    Properties = properties,
                    Timestamp = logEvent.Timestamp.UtcDateTime,
                    Message = message
                });
            }

            return logs;
        }
    }
}
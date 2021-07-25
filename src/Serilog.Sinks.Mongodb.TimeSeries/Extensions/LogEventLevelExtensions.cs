using System;
using System.Collections.Generic;
using System.Linq;
using Serilog.Events;
using Serilog.Sinks.Mongodb.TimeSeries.Models;

namespace Serilog.Sinks.Mongodb.TimeSeries.Extensions
{
    /// <summary>
    ///     Contains all extensions methods for <see cref="LogEventLevel" />.
    /// </summary>
    internal static class LogEventLevelExtensions
    {
        /// <summary>
        ///     Converts a <see cref="LogEventLevel" /> into a readable <see cref="string" />.
        /// </summary>
        /// <param name="level">The <see cref="LogEventLevel" />.</param>
        /// <returns>
        ///     The readable <see cref="string" /> containing the severity.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when no severity string was found for the current
        ///     <see cref="LogEventLevel" />.
        /// </exception>
        internal static string ToSeverityString(this LogEventLevel level)
        {
            return level switch
            {
                LogEventLevel.Verbose => "verbose",
                LogEventLevel.Debug => "debug",
                LogEventLevel.Information => "information",
                LogEventLevel.Warning => "warning",
                LogEventLevel.Error => "error",
                LogEventLevel.Fatal => "fatal",
                _ => throw new ArgumentOutOfRangeException(nameof(level), level, null)
            };
        }

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
                logs.Add(new LogDocument
                {
                    Exception = logEvent.Exception,
                    Severity = logEvent.Level.ToSeverityString(),
                    Properties = logEvent.Properties.ToDictionary(x => x.Key.ToSaveAbleString(), x => x.Value.ToString()),
                    Timestamp = logEvent.Timestamp.UtcDateTime,
                    Message = message
                });
            }

            return logs;
        }
    }
}
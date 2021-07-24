using System;
using Serilog.Events;

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
    }
}
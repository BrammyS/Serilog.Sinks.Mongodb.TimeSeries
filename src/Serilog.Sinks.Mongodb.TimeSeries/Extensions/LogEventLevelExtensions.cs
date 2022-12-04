using System;
using Serilog.Events;

namespace Serilog.Sinks.Mongodb.TimeSeries.Extensions;

/// <summary>
///     Contains all extensions methods for <see cref="LogEventLevel" />.
/// </summary>
internal static class LogEventLevelExtensions
{
    private const string Verbose = "verbose";
    private const string Debug = "debug";
    private const string Information = "information";
    private const string Warning = "warning";
    private const string Error = "error";
    private const string Fatal = "fatal";

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
            LogEventLevel.Verbose => Verbose,
            LogEventLevel.Debug => Debug,
            LogEventLevel.Information => Information,
            LogEventLevel.Warning => Warning,
            LogEventLevel.Error => Error,
            LogEventLevel.Fatal => Fatal,
            _ => throw new ArgumentOutOfRangeException(nameof(level), level, null)
        };
    }
}
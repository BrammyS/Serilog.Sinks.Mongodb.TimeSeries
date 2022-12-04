namespace Serilog.Sinks.Mongodb.TimeSeries.Extensions;

/// <summary>
///     Contains all extensions methods for <see cref="string" />.
/// </summary>
internal static class StringExtensions
{
    private const string NullTag = "[NULL]";
    private const char DotChar = '.';
    private const char LineChar = '-';
    private const char DollarChar = '$';
    private const char UnderscoreChar = '_';

    /// <summary>
    ///     Turn a <see cref="string" /> into a savable string.
    /// </summary>
    /// <param name="data">The <see cref="string" /> data.</param>
    /// <returns>
    ///     The savable <see cref="string" />.
    /// </returns>
    internal static string ToSaveAbleString(this string? data)
    {
        return data == null ? NullTag : data.Replace(DotChar, LineChar).Replace(DollarChar, UnderscoreChar);
    }
}
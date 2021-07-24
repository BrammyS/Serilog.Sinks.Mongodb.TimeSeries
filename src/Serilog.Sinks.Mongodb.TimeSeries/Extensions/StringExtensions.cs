namespace Serilog.Sinks.Mongodb.TimeSeries.Extensions
{
    /// <summary>
    ///     Contains all extensions methods for <see cref="string" />.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        ///     Turn a <see cref="string" /> into a savable string.
        /// </summary>
        /// <param name="data">The <see cref="string" /> data.</param>
        /// <returns>
        ///     The savable <see cref="string" />.
        /// </returns>
        internal static string ToSaveAbleString(this string? data)
        {
            return data == null ? "[NULL]" : data.Replace('.', '-').Replace('$', '_');
        }
    }
}
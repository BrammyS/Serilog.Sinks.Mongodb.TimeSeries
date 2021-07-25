using System;
using MongoDB.Driver;
using Serilog.Configuration;
using Serilog.Sinks.Mongodb.TimeSeries.Configurations;
using Serilog.Sinks.PeriodicBatching;

namespace Serilog.Sinks.Mongodb.TimeSeries.Extensions
{
    /// <summary>
    ///     Makes the WriteTo.MongoDbTimeSeriesSink() extension method available to <see cref="LoggerConfiguration" />.
    /// </summary>
    public static class LoggerSinkConfigurationExtensions
    {
        /// <summary>
        ///     Registers the MongoDb time series sink to the logger.
        /// </summary>
        /// <param name="loggerConfiguration">The <see cref="LoggerSinkConfiguration" />.</param>
        /// <param name="sinkConfig">The configurations the the mongodb time series sink will use.</param>
        /// <param name="batchingOptions">
        ///     The <see cref="PeriodicBatchingSinkOptions" /> containing the settings that will be used
        ///     to batch the logs.
        /// </param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <returns>
        ///     The new <see cref="LoggerSinkConfiguration" /> with the new sink.
        /// </returns>
        public static LoggerConfiguration MongoDbTimeSeriesSink(this LoggerSinkConfiguration loggerConfiguration, MongoDbTimeSeriesSinkConfig sinkConfig,
                                                                PeriodicBatchingSinkOptions batchingOptions, IFormatProvider? formatProvider = null)
        {
            var batchingMongoDbSink = new PeriodicBatchingSink(new MongoDbTimeSink(sinkConfig, formatProvider), batchingOptions);
            return loggerConfiguration.Sink(batchingMongoDbSink);
        }

        /// <summary>
        ///     Registers the MongoDb time series sink to the logger.
        /// </summary>
        /// <param name="loggerConfiguration">The <see cref="LoggerSinkConfiguration" />.</param>
        /// <param name="database">The database of where the logs will be stored.</param>
        /// <param name="batchingOptions">
        ///     The <see cref="PeriodicBatchingSinkOptions" /> containing the settings that will be used
        ///     to batch the logs.
        /// </param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <returns>
        ///     The new <see cref="LoggerSinkConfiguration" /> with the new sink.
        /// </returns>
        public static LoggerConfiguration MongoDbTimeSeriesSink(this LoggerSinkConfiguration loggerConfiguration, IMongoDatabase database,
                                                                PeriodicBatchingSinkOptions batchingOptions, IFormatProvider? formatProvider = null)
        {
            return MongoDbTimeSeriesSink(loggerConfiguration, new MongoDbTimeSeriesSinkConfig(database), batchingOptions, formatProvider);
        }

        /// <summary>
        ///     Registers the MongoDb time series sink to the logger.
        /// </summary>
        /// <param name="loggerConfiguration">The <see cref="LoggerSinkConfiguration" />.</param>
        /// <param name="database">The database of where the logs will be stored.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <returns>
        ///     The new <see cref="LoggerSinkConfiguration" /> with the new sink.
        /// </returns>
        public static LoggerConfiguration MongoDbTimeSeriesSink(this LoggerSinkConfiguration loggerConfiguration, IMongoDatabase database, IFormatProvider? formatProvider = null)
        {
            var defaultSinkOptions = new PeriodicBatchingSinkOptions
            {
                BatchSizeLimit = 500,
                Period = TimeSpan.FromSeconds(10),
                EagerlyEmitFirstEvent = true
            };
            return MongoDbTimeSeriesSink(loggerConfiguration, database, defaultSinkOptions, formatProvider);
        }
    }
}